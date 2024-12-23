using MediatR;
using TravelAndAccommodationBookingPlatform.Application.DTOs.ReviewsDtos;

namespace TravelAndAccommodationBookingPlatform.Application.Queries.ReviewQueries
{
    public class GetReviewByIdQuery : IRequest<ReviewResponseDto>
    {
        public Guid ReviewId { get; init; }
        public Guid HotelId { get; init; }
    }
}
