using MediatR;

namespace TravelAndAccommodationBookingPlatform.Application.Commands.RoomCommands
{
    public class UpdateRoomCommand : IRequest
    {
        public Guid RoomClassId { get; init; }
        public Guid RoomId { get; init; }
        public string Number { get; init; }
    }
}
