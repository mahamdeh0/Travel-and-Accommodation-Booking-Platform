using MediatR;
using TravelAndAccommodationBookingPlatform.Application.DTOs.CityDtos;

namespace TravelAndAccommodationBookingPlatform.Application.Commands.CityCommands
{
    public class CreateCityCommand : IRequest<CityResponseDto>
    {
        public string Name { get; init; }
        public string Country { get; init; }
        public string Region { get; set; }
        public string PostOffice { get; init; }
    }
}
