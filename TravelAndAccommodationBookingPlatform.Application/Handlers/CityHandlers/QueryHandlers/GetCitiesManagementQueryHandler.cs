using AutoMapper;
using MediatR;
using System.Linq.Expressions;
using TravelAndAccommodationBookingPlatform.Application.DTOs.CityDtos;
using TravelAndAccommodationBookingPlatform.Application.Queries.CityQueries;
using TravelAndAccommodationBookingPlatform.Core.Entities;
using TravelAndAccommodationBookingPlatform.Core.Enums;
using TravelAndAccommodationBookingPlatform.Core.Interfaces.Repositories;
using TravelAndAccommodationBookingPlatform.Core.Models;

namespace TravelAndAccommodationBookingPlatform.Application.Handlers.CityHandlers.QueryHandlers
{
    public class GetCitiesManagementQueryHandler : IRequestHandler<GetCitiesManagementQuery, PaginatedResult<CityManagementResponseDto>>
    {
        private readonly ICityRepository _cityRepository;
        private readonly IMapper _mapper;

        public GetCitiesManagementQueryHandler(ICityRepository cityRepository, IMapper mapper)
        {
            _cityRepository = cityRepository;
            _mapper = mapper;
        }

        public async Task<PaginatedResult<CityManagementResponseDto>> Handle(GetCitiesManagementQuery request, CancellationToken cancellationToken)
        {
            Expression<Func<City, bool>> filterExpression = string.IsNullOrEmpty(request.Search)
                 ? _ => true : city => city.Name.Contains(request.Search) || city.Country.Contains(request.Search);

            var query = new PaginatedQuery<City>(
               filterExpression,
               request.SortColumn,
               request.PageNumber,
               request.PageSize,
               request.OrderDirection ?? OrderDirection.Ascending
           );

            var City = await _cityRepository.GetCitiesForAdminAsync(query);
            return _mapper.Map<PaginatedResult<CityManagementResponseDto>>(City);

        }
    }
}
