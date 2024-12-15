using MediatR;
using Microsoft.AspNetCore.Http;

namespace TravelAndAccommodationBookingPlatform.Application.Commands.HotelCommands
{
    public class AddHotelThumbnailCommand : IRequest
    {
        public Guid HotelId { get; init; }
        public IFormFile Image { get; init; }
    }
}
