using MediatR;
using TravelAndAccommodationBookingPlatform.Application.DTOs.RoomClassDtos;
using TravelAndAccommodationBookingPlatform.Core.Enums;
using TravelAndAccommodationBookingPlatform.Core.Models;

namespace TravelAndAccommodationBookingPlatform.Application.Queries.RoomClassQueries
{
    public class GetRoomClassGuestQuery : IRequest<PaginatedResult<RoomClassGuestResponseDto>>
    {
        public Guid HotelId { get; init; }
        public int PageNumber { get; init; }
        public int PageSize { get; init; }
        public OrderDirection? OrderDirection { get; init; }
        public string? SortColumn { get; init; }
    }
}
