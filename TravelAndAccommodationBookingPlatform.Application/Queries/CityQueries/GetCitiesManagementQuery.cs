using MediatR;
using TravelAndAccommodationBookingPlatform.Application.DTOs.CityDtos;
using TravelAndAccommodationBookingPlatform.Core.Enums;
using TravelAndAccommodationBookingPlatform.Core.Models;

namespace TravelAndAccommodationBookingPlatform.Application.Queries.CityQueries
{
    public class GetCitiesManagementQuery : IRequest<PaginatedResult<CityManagementResponseDto>>
    {
        public OrderDirection? OrderDirection { get; init; }
        public string? SortColumn { get; init; }
        public string? Search { get; init; }
        public int PageNumber { get; init; }
        public int PageSize { get; init; }
    }
}
