using MediatR;

namespace TravelAndAccommodationBookingPlatform.Application.Commands.ReviewCommands
{
    public class DeleteReviewCommand : IRequest
    {
        public Guid HotelId { get; init; }
        public Guid ReviewId { get; init; }
    }
}
