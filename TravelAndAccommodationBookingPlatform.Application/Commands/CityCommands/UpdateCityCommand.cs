using MediatR;

namespace TravelAndAccommodationBookingPlatform.Application.Commands.CityCommands
{
    public class UpdateCityCommand : IRequest
    {
        public Guid CityId { get; init; }
        public string Name { get; init; }
        public string Country { get; init; }
        public string Region { get; init; }
        public string PostOffice { get; init; }
    }
}
