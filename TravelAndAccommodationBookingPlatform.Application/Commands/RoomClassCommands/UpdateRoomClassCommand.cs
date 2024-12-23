using MediatR;
using TravelAndAccommodationBookingPlatform.Core.Enums;

namespace TravelAndAccommodationBookingPlatform.Application.Commands.RoomClassCommands
{
    public class UpdateRoomClassCommand : IRequest
    {
        public Guid RoomClassId { get; init; }
        public string Name { get; init; }
        public int MaxChildrenCapacity { get; init; }
        public int MaxAdultsCapacity { get; init; }
        public RoomType TypeOfRoom { get; init; }
        public decimal NightlyRate { get; init; }
        public string? Description { get; init; }
    }
}
