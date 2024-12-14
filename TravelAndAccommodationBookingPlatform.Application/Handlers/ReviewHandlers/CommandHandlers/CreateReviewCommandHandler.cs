using AutoMapper;
using MediatR;
using System.ComponentModel.DataAnnotations;
using TravelAndAccommodationBookingPlatform.Application.Commands.ReviewCommands;
using TravelAndAccommodationBookingPlatform.Application.DTOs.ReviewsDtos;
using TravelAndAccommodationBookingPlatform.Core.DomainMessages;
using TravelAndAccommodationBookingPlatform.Core.Entities;
using TravelAndAccommodationBookingPlatform.Core.Exceptions;
using TravelAndAccommodationBookingPlatform.Core.Interfaces.Repositories;
using TravelAndAccommodationBookingPlatform.Core.Interfaces.Services;
using TravelAndAccommodationBookingPlatform.Core.Interfaces.UnitOfWork;

namespace TravelAndAccommodationBookingPlatform.Application.Handlers.ReviewHandlers.CommandHandlers
{
    public class CreateReviewCommandHandler : IRequestHandler<CreateReviewCommand, ReviewResponseDto>
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IReviewRepository _reviewRepository;
        private readonly IHotelRepository _hotelRepository;
        private readonly IUserSession _userSession;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateReviewCommandHandler(IBookingRepository bookingRepository, IReviewRepository reviewRepository, IHotelRepository hotelRepository, IUserSession userSession, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _bookingRepository = bookingRepository;
            _reviewRepository = reviewRepository;
            _hotelRepository = hotelRepository;
            _userSession = userSession;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ReviewResponseDto> Handle(CreateReviewCommand request, CancellationToken cancellationToken)
        {
            var userId = _userSession.GetUserId();
            var userRole = _userSession.GetUserRole();

            if (userRole != "Guest")
                throw new ForbiddenException(UserMessages.UserNotGuest);

            if (request.Rating < 1 || request.Rating > 5)
                throw new ValidationException(ReviewMessages.InvalidReviewRating);

            var hotelExists = await _hotelRepository.ExistsByPredicateAsync(h => h.Id == request.HotelId);
            if (!hotelExists)
                throw new NotFoundException(HotelMessages.HotelNotFound);

            var hasBooking = await _bookingRepository.ExistsByPredicateAsync(b => b.HotelId == request.HotelId && b.GuestId == userId);
            if (!hasBooking)
                throw new GuestNotBookedHotelException(BookingMessages.NoBookingsForGuestInHotel);

            var alreadyReviewed = await _reviewRepository.ExistsAsync(r => r.HotelId == request.HotelId && r.GuestId == userId);
            if (alreadyReviewed)
                throw new ReviewAlreadyExistsException(ReviewMessages.GuestAlreadyReviewedHotel);

            var totalRating = await _reviewRepository.GetHotelRatingAsync(request.HotelId);
            var reviewCount = await _reviewRepository.GetHotelReviewCountAsync(request.HotelId);

            var newRating = (totalRating + request.Rating) / (reviewCount + 1);
            await _hotelRepository.UpdateReviewById(request.HotelId, newRating);

            var reviewEntity = _mapper.Map<Review>(request);
            reviewEntity.GuestId = userId;
            reviewEntity.CreatedAt = DateTime.Now;

            var createdReview = await _reviewRepository.AddReviewAsync(reviewEntity);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<ReviewResponseDto>(createdReview);
        }
    }
}
