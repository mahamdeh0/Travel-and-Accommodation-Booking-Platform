using MediatR;
using TravelAndAccommodationBookingPlatform.Application.DTOs.HotelDtos;

namespace TravelAndAccommodationBookingPlatform.Application.Queries.HotelQueries
{
    public class GetRecentlyVisitedHotelsQuery : IRequest<IEnumerable<RecentlyVisitedHotelResponseDto>>
    {
        public Guid GuestId { get; init; }
        public int Count { get; init; }
    }
}
