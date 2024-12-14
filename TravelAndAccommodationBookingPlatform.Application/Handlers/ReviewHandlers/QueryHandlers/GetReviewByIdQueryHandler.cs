using AutoMapper;
using MediatR;
using TravelAndAccommodationBookingPlatform.Application.DTOs.ReviewsDtos;
using TravelAndAccommodationBookingPlatform.Application.Queries.ReviewQueries;
using TravelAndAccommodationBookingPlatform.Core.DomainMessages;
using TravelAndAccommodationBookingPlatform.Core.Exceptions;
using TravelAndAccommodationBookingPlatform.Core.Interfaces.Repositories;

namespace TravelAndAccommodationBookingPlatform.Application.Handlers.ReviewHandlers.QueryHandlers
{
    public class GetReviewByIdQueryHandler : IRequestHandler<GetReviewByIdQuery, ReviewResponseDto>
    {
        private readonly IReviewRepository _reviewRepository;
        private readonly IHotelRepository _hotelRepository;
        private readonly IMapper _mapper;

        public GetReviewByIdQueryHandler(IReviewRepository reviewRepository, IHotelRepository hotelRepository, IMapper mapper)
        {
            _reviewRepository = reviewRepository;
            _hotelRepository = hotelRepository;
            _mapper = mapper;
        }

        public async Task<ReviewResponseDto> Handle(GetReviewByIdQuery request, CancellationToken cancellationToken)
        {
            var hotelExists = await _hotelRepository.ExistsByPredicateAsync(h => h.Id == request.HotelId);
            if (!hotelExists)
                throw new NotFoundException(HotelMessages.HotelNotFound);

            var review = await _reviewRepository.GetReviewByIdAsync(request.HotelId, request.ReviewId);
            if (review == null)
                throw new NotFoundException(ReviewMessages.ReviewNotFoundForHotel);

            return _mapper.Map<ReviewResponseDto>(review);
        }
    }
}
