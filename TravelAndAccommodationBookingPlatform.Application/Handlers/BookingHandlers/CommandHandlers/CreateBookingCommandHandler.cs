using AutoMapper;
using MediatR;
using TravelAndAccommodationBookingPlatform.Application.Commands.BookingCommands;
using TravelAndAccommodationBookingPlatform.Application.DTOs.BookingDtos;
using TravelAndAccommodationBookingPlatform.Application.Handlers.BookingHandlers.Shared;
using TravelAndAccommodationBookingPlatform.Core.DomainMessages;
using TravelAndAccommodationBookingPlatform.Core.Entities;
using TravelAndAccommodationBookingPlatform.Core.Exceptions;
using TravelAndAccommodationBookingPlatform.Core.Interfaces.Repositories;
using TravelAndAccommodationBookingPlatform.Core.Interfaces.Services;
using TravelAndAccommodationBookingPlatform.Core.Interfaces.UnitOfWork;
using TravelAndAccommodationBookingPlatform.Core.Models;

namespace TravelAndAccommodationBookingPlatform.Application.Handlers.BookingHandlers.CommandHandlers
{
    public class CreateBookingCommandHandler : IRequestHandler<CreateBookingCommand, BookingResponseDto>
    {
        private readonly IPdfGeneratorService _pdfGeneratorService;
        private readonly IBookingRepository _bookingRepository;
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly IHotelRepository _hotelRepository;
        private readonly IUserRepository _userRepository;
        private readonly IRoomRepository _roomRepository;
        private readonly IEmailService _emailService;
        private readonly IUserSession _userSession;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateBookingCommandHandler(IPdfGeneratorService pdfGeneratorService, IBookingRepository bookingRepository, IDateTimeProvider dateTimeProvider, IHotelRepository hotelRepository, IUserRepository userRepository, IRoomRepository roomRepository, IEmailService emailService, IUserSession userSession, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _pdfGeneratorService = pdfGeneratorService;
            _bookingRepository = bookingRepository;
            _dateTimeProvider = dateTimeProvider;
            _hotelRepository = hotelRepository;
            _userRepository = userRepository;
            _roomRepository = roomRepository;
            _emailService = emailService;
            _userSession = userSession;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BookingResponseDto> Handle(CreateBookingCommand request, CancellationToken cancellationToken)
        {
            var userId = _userSession.GetUserId();
            var userRole = _userSession.GetUserRole();
            var userEmail = _userSession.GetUserEmail();

            var user = await _userRepository.GetUserByIdAsync(userId);
            if (user == null)
                throw new NotFoundException(UserMessages.UserNotFound);

            if (userRole != "Guest")
                throw new ForbiddenException(UserMessages.UserNotGuest);

            var hotel = await _hotelRepository.GetHotelByIdAsync(request.HotelId);
            if (hotel == null)
                throw new NotFoundException(HotelMessages.HotelNotFound);

            var rooms = new List<Room>();
            foreach (var roomId in request.RoomId)
            {
                var room = await _roomRepository.GetRoomWithRoomClassByIdAsync(roomId);
                if (room == null)
                    throw new NotFoundException(RoomMessages.RoomNotFound);

                if (room.RoomClass.HotelId != request.HotelId)
                    throw new RoomsNotInHotelException(RoomMessages.RoomNotFound);


                bool isAvailable = await IsRoomAvailableAsync(room, request.CheckInDate, request.CheckOutDate);
                if (!isAvailable)
                    throw new RoomNotAvailableException(RoomMessages.RoomUnavailable(roomId));

                rooms.Add(room);
            }

            var totalPrice = CalculateTotalPrice(rooms, request.CheckInDate, request.CheckOutDate);

            var booking = new Booking
            {
                GuestId = userId,
                CheckInDate = request.CheckInDate,
                CheckOutDate = request.CheckOutDate,
                PaymentType = request.PaymentType,
                GuestRemarks = request.GuestRemarks,
                TotalPrice = totalPrice,
                BookingDate = DateOnly.FromDateTime(_dateTimeProvider.Now),
                Hotel = hotel,
                Rooms = rooms
            };

            var createdBooking = await _bookingRepository.AddBookingAsync(booking);

            foreach (var room in rooms)
            {
                var discountPercentage = room.RoomClass.Discounts.FirstOrDefault()?.Percentage ?? 0;
                var priceAtReservation = room.RoomClass.NightlyRate;
                var amountAfterDiscount = priceAtReservation * (100 - discountPercentage) / 100;

                var invoiceDetail = new InvoiceDetail
                {
                    RoomId = room.Id,
                    BookingId = createdBooking.Id,
                    Booking = createdBooking,
                    RoomClassName = room.RoomClass.Name,
                    RoomNumber = room.Number,
                    DiscountAppliedAtBooking = discountPercentage,
                    PriceAtReservation = priceAtReservation,
                    AmountAfterDiscount = amountAfterDiscount,
                    TotalAmount = amountAfterDiscount
                };

                createdBooking.Invoice.Add(invoiceDetail);
            }

            await _unitOfWork.SaveChangesAsync();

            string invoiceHtml = InvoiceHtmlGenerator.GenerateInvoiceHtml(createdBooking);

            var pdfBytes = await _pdfGeneratorService.GeneratePdfFromHtmlAsync(invoiceHtml);

            var emailRequest = new EmailRequest(
                ToEmails: new[] { userEmail },
                SubjectLine: "Your Booking Invoice",
                MessageBody: "Thank you for your booking. Please find the invoice attached.",
                FileAttachment: new[]
                {
                        new FileAttachment(
                            FileName: "invoice.pdf",
                            MediaType: "application/pdf",
                            FileContent: pdfBytes
                        )
                }
            );

            await _emailService.SendAsync(emailRequest);

            return _mapper.Map<BookingResponseDto>(createdBooking);
        }

        private async Task<bool> IsRoomAvailableAsync(Room room, DateOnly checkIn, DateOnly checkOut)
        {
            bool overlappingBookingExists = await _bookingRepository.ExistsByPredicateAsync(b =>
                b.Rooms.Any(r => r.Id == room.Id) &&
                !(b.CheckOutDate <= checkIn || b.CheckInDate >= checkOut));

            return !overlappingBookingExists;
        }

        private static decimal CalculateTotalPrice(IEnumerable<Room> rooms, DateOnly checkIn, DateOnly checkOut)
        {
            int nights = (checkOut.ToDateTime(TimeOnly.MinValue) - checkIn.ToDateTime(TimeOnly.MinValue)).Days;
            if (nights < 1) nights = 1;

            var totalPerNight = rooms.Sum(room =>
            {
                decimal basePrice = room.RoomClass.NightlyRate;
                var discountPercentage = room.RoomClass.Discounts.FirstOrDefault()?.Percentage ?? 0;
                decimal discountedPrice = basePrice * (100 - discountPercentage) / 100;
                return discountedPrice;
            });

            return totalPerNight * nights;
        }

    }
}
