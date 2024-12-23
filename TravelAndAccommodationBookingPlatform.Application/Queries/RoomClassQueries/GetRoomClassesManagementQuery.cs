using MediatR;
using TravelAndAccommodationBookingPlatform.Application.DTOs.RoomClassDtos;
using TravelAndAccommodationBookingPlatform.Core.Enums;
using TravelAndAccommodationBookingPlatform.Core.Models;

namespace TravelAndAccommodationBookingPlatform.Application.Queries.RoomClassQueries
{
    public class GetRoomClassesManagementQuery : IRequest<PaginatedResult<RoomClassManagementResponseDto>>
    {
        public int PageNumber { get; init; }
        public int PageSize { get; init; }
        public string? Search { get; init; }
        public OrderDirection? OrderDirection { get; init; }
        public string? SortColumn { get; init; }
    }
}
