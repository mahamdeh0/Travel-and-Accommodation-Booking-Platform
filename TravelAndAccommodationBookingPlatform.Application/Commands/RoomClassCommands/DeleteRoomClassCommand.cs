using MediatR;

namespace TravelAndAccommodationBookingPlatform.Application.Commands.RoomClassCommands
{
    public class DeleteRoomClassCommand : IRequest
    {
        public Guid RoomClassId { get; init; }
    }
}
