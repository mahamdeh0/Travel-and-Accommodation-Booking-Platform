using MediatR;
using TravelAndAccommodationBookingPlatform.Application.DTOs.ReviewsDtos;

namespace TravelAndAccommodationBookingPlatform.Application.Commands.ReviewCommands
{
    public class CreateReviewCommand : IRequest<ReviewResponseDto>
    {
        public Guid HotelId { get; init; }
        public int Rating { get; init; }
        public string Content { get; init; }
    }
}
