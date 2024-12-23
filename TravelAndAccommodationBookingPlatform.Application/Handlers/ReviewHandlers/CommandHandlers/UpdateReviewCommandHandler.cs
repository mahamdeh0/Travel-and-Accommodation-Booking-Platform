using AutoMapper;
using MediatR;
using System.ComponentModel.DataAnnotations;
using TravelAndAccommodationBookingPlatform.Application.Commands.ReviewCommands;
using TravelAndAccommodationBookingPlatform.Core.DomainMessages;
using TravelAndAccommodationBookingPlatform.Core.Entities;
using TravelAndAccommodationBookingPlatform.Core.Exceptions;
using TravelAndAccommodationBookingPlatform.Core.Interfaces.Repositories;
using TravelAndAccommodationBookingPlatform.Core.Interfaces.Services;
using TravelAndAccommodationBookingPlatform.Core.Interfaces.UnitOfWork;

namespace TravelAndAccommodationBookingPlatform.Application.Handlers.ReviewHandlers.CommandHandlers
{
    public class UpdateReviewCommandHandler : IRequestHandler<UpdateReviewCommand>
    {
        private readonly IReviewRepository _reviewRepository;
        private readonly IHotelRepository _hotelRepository;
        private readonly IUserRepository _userRepository;
        private readonly IUserSession _userSession;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateReviewCommandHandler(
            IReviewRepository reviewRepository,
            IHotelRepository hotelRepository,
            IUserRepository userRepository,
            IUserSession userSession,
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _reviewRepository = reviewRepository;
            _hotelRepository = hotelRepository;
            _userRepository = userRepository;
            _userSession = userSession;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task Handle(UpdateReviewCommand request, CancellationToken cancellationToken)
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

            var review = await _reviewRepository.GetReviewByIdAsync(request.ReviewId, request.HotelId, userId);
            if (review == null)
                throw new NotFoundException(ReviewMessages.ReviewNotFoundForUserAndHotel);

            _mapper.Map(request, review);
            review.UpdatedAt = DateTime.Now;

            await _reviewRepository.UpdateReviewAsync(review);

            var totalRating = await _reviewRepository.GetHotelRatingAsync(request.HotelId);
            var reviewCount = await _reviewRepository.GetHotelReviewCountAsync(request.HotelId);
            var newRating = (totalRating + request.Rating) / reviewCount;

            await _hotelRepository.UpdateReviewById(request.HotelId, newRating);

            await _unitOfWork.SaveChangesAsync();
        }
    }
}
