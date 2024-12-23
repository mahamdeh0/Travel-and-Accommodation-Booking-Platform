using MediatR;

namespace TravelAndAccommodationBookingPlatform.Application.Commands.BookingCommands
{
    public class DeleteBookingCommand : IRequest
    {
        public Guid BookingId { get; init; }
    }
}
