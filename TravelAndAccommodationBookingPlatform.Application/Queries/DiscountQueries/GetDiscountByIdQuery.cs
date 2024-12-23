using MediatR;
using TravelAndAccommodationBookingPlatform.Application.DTOs.DiscountDtos;

namespace TravelAndAccommodationBookingPlatform.Application.Queries.DiscountQueries
{
    public class GetDiscountByIdQuery : IRequest<DiscountResponseDto>
    {
        public Guid RoomClassId { get; init; }
        public Guid DiscountId { get; init; }
    }
}
