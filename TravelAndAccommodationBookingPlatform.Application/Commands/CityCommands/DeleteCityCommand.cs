using MediatR;

namespace TravelAndAccommodationBookingPlatform.Application.Commands.CityCommands
{
    public class DeleteCityCommand : IRequest
    {
        public Guid CityId { get; init; }
    }
}
