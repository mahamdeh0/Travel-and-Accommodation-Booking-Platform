using MediatR;
using TravelAndAccommodationBookingPlatform.Application.DTOs.RoomDtos;
using TravelAndAccommodationBookingPlatform.Core.Enums;
using TravelAndAccommodationBookingPlatform.Core.Models;

namespace TravelAndAccommodationBookingPlatform.Application.Queries.RoomQueries
{
    public class GetRoomsManagementQuery : IRequest<PaginatedResult<RoomManagementResponseDto>>
    {
        public Guid RoomClassId { get; init; }
        public OrderDirection? OrderDirection { get; init; }
        public string? Search { get; init; }
        public string? SortColumn { get; init; }
        public int PageNumber { get; init; }
        public int PageSize { get; init; }
    }
}
