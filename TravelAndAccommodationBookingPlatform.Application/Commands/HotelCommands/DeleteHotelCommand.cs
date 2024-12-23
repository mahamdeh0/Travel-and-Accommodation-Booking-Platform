using MediatR;

namespace TravelAndAccommodationBookingPlatform.Application.Commands.HotelCommands
{
    public class DeleteHotelCommand : IRequest
    {
        public Guid HotelId { get; init; }
    }
}
