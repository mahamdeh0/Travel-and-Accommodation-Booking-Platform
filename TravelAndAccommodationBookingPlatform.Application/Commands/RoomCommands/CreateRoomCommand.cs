using MediatR;

namespace TravelAndAccommodationBookingPlatform.Application.Commands.RoomCommands
{
    public class CreateRoomCommand : IRequest<Guid>
    {
        public Guid RoomClassId { get; init; }
        public string Number { get; init; }
    }
}
