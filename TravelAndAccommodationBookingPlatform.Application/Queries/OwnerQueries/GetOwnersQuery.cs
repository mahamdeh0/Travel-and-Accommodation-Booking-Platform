using MediatR;
using TravelAndAccommodationBookingPlatform.Application.DTOs.OwnerDtos;
using TravelAndAccommodationBookingPlatform.Core.Enums;
using TravelAndAccommodationBookingPlatform.Core.Models;

namespace TravelAndAccommodationBookingPlatform.Application.Queries.OwnerQueries
{
    public class GetOwnersQuery : IRequest<PaginatedResult<OwnerResponseDto>>
    {
        public int PageNumber { get; init; }
        public int PageSize { get; init; }
        public string? Search { get; init; }
        public OrderDirection? OrderDirection { get; init; }
        public string? SortColumn { get; init; }
    }
}
