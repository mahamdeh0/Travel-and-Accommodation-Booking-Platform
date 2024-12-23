using MediatR;
using Microsoft.AspNetCore.Http;

namespace TravelAndAccommodationBookingPlatform.Application.Commands.RoomClassCommands
{
    public class AddGalleryToRoomClassCommand : IRequest
    {
        public Guid RoomClassId { get; init; }
        public IFormFile Image { get; init; }
    }
}
