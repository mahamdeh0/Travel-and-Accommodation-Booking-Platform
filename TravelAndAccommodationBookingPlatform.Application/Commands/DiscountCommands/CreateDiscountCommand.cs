using MediatR;
using TravelAndAccommodationBookingPlatform.Application.DTOs.DiscountDtos;

namespace TravelAndAccommodationBookingPlatform.Application.Commands.DiscountCommands
{
    public class CreateDiscountCommand : IRequest<DiscountResponseDto>
    {
        public Guid RoomClassId { get; init; }
        public decimal Percentage { get; init; }
        public DateTime StartDate { get; init; }
        public DateTime EndDate { get; init; }
    }
}
