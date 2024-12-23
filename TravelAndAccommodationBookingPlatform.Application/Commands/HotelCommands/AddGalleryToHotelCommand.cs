using MediatR;
using Microsoft.AspNetCore.Http;

namespace TravelAndAccommodationBookingPlatform.Application.Commands.HotelCommands
{
    public class AddGalleryToHotelCommand : IRequest
    {
        public Guid HotelId { get; init; }
        public IFormFile Image { get; init; }
    }
}
