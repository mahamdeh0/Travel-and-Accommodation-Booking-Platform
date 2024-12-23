using MediatR;
using Microsoft.AspNetCore.Http;

namespace TravelAndAccommodationBookingPlatform.Application.Commands.CityCommands
{
    public class AddCityThumbnailCommand : IRequest
    {
        public Guid CityId { get; init; }
        public IFormFile Image { get; init; }
    }
}
