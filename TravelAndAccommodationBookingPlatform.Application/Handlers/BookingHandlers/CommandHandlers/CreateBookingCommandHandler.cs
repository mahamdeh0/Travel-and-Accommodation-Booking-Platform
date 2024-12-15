using AutoMapper;
using MediatR;
using TravelAndAccommodationBookingPlatform.Application.Commands.BookingCommands;
using TravelAndAccommodationBookingPlatform.Application.DTOs.BookingDtos;
using TravelAndAccommodationBookingPlatform.Core.Interfaces.Repositories;
using TravelAndAccommodationBookingPlatform.Core.Interfaces.Services;
using TravelAndAccommodationBookingPlatform.Core.Interfaces.UnitOfWork;

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
            throw new NotImplementedException();
        }
    }
}
