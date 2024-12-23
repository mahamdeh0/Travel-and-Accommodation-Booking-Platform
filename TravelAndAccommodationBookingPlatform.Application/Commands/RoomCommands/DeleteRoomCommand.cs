using MediatR;

namespace TravelAndAccommodationBookingPlatform.Application.Commands.RoomCommands
{
    public class DeleteRoomCommand : IRequest
    {
        public Guid RoomClassId { get; init; }
        public Guid RoomId { get; init; }
    }
}
