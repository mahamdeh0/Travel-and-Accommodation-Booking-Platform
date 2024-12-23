using MediatR;

namespace TravelAndAccommodationBookingPlatform.Application.Commands.ReviewCommands
{
    public class UpdateReviewCommand : IRequest
    {
        public Guid ReviewId { get; init; }
        public Guid HotelId { get; init; }
        public int Rating { get; init; }
        public string Content { get; init; }
    }
}
