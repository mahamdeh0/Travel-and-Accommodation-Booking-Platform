using AutoMapper;
using MediatR;
using TravelAndAccommodationBookingPlatform.Application.DTOs.CityDtos;
using TravelAndAccommodationBookingPlatform.Application.Queries.CityQueries;
using TravelAndAccommodationBookingPlatform.Core.Interfaces.Repositories;

namespace TravelAndAccommodationBookingPlatform.Application.Handlers.CityHandlers.QueryHandlers
{
    public class GetTrendingCitiesQueryHandler : IRequestHandler<GetTrendingCitiesQuery, IEnumerable<TrendingCityResponseDto>>
    {
        private readonly ICityRepository _cityRepository;
        private readonly IMapper _mapper;

        public GetTrendingCitiesQueryHandler(ICityRepository cityRepository, IMapper mapper)
        {
            _cityRepository = cityRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TrendingCityResponseDto>> Handle(GetTrendingCitiesQuery request, CancellationToken cancellationToken)
        {
            if (request.Count <= 0)
                throw new ArgumentException();

            var trendingCities = await _cityRepository.GetTopMostVisitedAsync(request.Count);
            return _mapper.Map<IEnumerable<TrendingCityResponseDto>>(trendingCities);
        }
    }
}

