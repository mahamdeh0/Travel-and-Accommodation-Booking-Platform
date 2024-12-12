using MediatR;
using TravelAndAccommodationBookingPlatform.Application.DTOs.BookingDtos;
using TravelAndAccommodationBookingPlatform.Core.Enums;
using TravelAndAccommodationBookingPlatform.Core.Models;

namespace TravelAndAccommodationBookingPlatform.Application.Queries.BookingQueries
{
    public class GetBookingsQuery : IRequest<PaginatedResult<BookingResponseDto>>
    {
        public string? SortColumn { get; init; }
        public int PageNumber { get; init; }
        public int PageSize { get; init; }
        public OrderDirection? OrderDirection { get; init; }
    }
}
