using MediatR;
using TravelAndAccommodationBookingPlatform.Application.DTOs.ReviewsDtos;
using TravelAndAccommodationBookingPlatform.Core.Enums;
using TravelAndAccommodationBookingPlatform.Core.Models;

namespace TravelAndAccommodationBookingPlatform.Application.Queries.ReviewQueries
{
    public class GetReviewsByHotelIdQuery : IRequest<PaginatedResult<ReviewResponseDto>>
    {
        public Guid HotelId { get; init; }
        public int PageNumber { get; init; }
        public int PageSize { get; init; }
        public OrderDirection? OrderDirection { get; init; }
        public string? SortColumn { get; init; }
    }
}
