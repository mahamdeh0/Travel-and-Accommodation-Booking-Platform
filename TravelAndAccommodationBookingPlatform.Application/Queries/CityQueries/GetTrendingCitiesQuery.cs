using MediatR;
using TravelAndAccommodationBookingPlatform.Application.DTOs.CityDtos;

namespace TravelAndAccommodationBookingPlatform.Application.Queries.CityQueries
{
    public class GetTrendingCitiesQuery : IRequest<IEnumerable<TrendingCityResponseDto>>
    {
        public int Count { get; init; }
    }
}
