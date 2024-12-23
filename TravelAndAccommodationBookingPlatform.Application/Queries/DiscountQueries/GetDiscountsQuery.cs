using MediatR;
using TravelAndAccommodationBookingPlatform.Application.DTOs.DiscountDtos;
using TravelAndAccommodationBookingPlatform.Core.Enums;
using TravelAndAccommodationBookingPlatform.Core.Models;

namespace TravelAndAccommodationBookingPlatform.Application.Queries.DiscountQueries
{
    public class GetDiscountsQuery : IRequest<PaginatedResult<DiscountResponseDto>>
    {
        public Guid RoomClassId { get; init; }
        public OrderDirection? OrderDirection { get; init; }
        public string? SortColumn { get; init; }
        public int PageNumber { get; init; }
        public int PageSize { get; init; }
    }
}
