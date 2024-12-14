using MediatR;

namespace TravelAndAccommodationBookingPlatform.Application.Commands.DiscountCommands
{
    public class DeleteDiscountCommand : IRequest
    {
        public Guid RoomClassId { get; init; }
        public Guid DiscountId { get; init; }
    }
}
