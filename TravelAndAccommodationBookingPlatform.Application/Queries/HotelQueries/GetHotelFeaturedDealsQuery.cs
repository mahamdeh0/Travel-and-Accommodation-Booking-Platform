using MediatR;
using TravelAndAccommodationBookingPlatform.Application.DTOs.HotelDtos;

namespace TravelAndAccommodationBookingPlatform.Application.Queries.HotelQueries
{
    public class GetHotelFeaturedDealsQuery : IRequest<IEnumerable<HotelFeaturedDealResponseDto>>
    {
        public int Count { get; init; }
    }
}
