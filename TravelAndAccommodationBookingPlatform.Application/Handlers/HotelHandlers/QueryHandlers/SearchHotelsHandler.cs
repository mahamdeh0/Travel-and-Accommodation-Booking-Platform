using AutoMapper;
using MediatR;
using System.Linq.Expressions;
using TravelAndAccommodationBookingPlatform.Application.DTOs.HotelDtos;
using TravelAndAccommodationBookingPlatform.Application.Queries.HotelQueries;
using TravelAndAccommodationBookingPlatform.Core.Entities;
using TravelAndAccommodationBookingPlatform.Core.Enums;
using TravelAndAccommodationBookingPlatform.Core.Interfaces.Repositories;
using TravelAndAccommodationBookingPlatform.Core.Models;

namespace TravelAndAccommodationBookingPlatform.Application.Handlers.HotelHandlers.QueryHandlers
{
    public class SearchForHotelsHandler : IRequestHandler<SearchHotelsQuery, PaginatedResult<HotelSearchResultResponseDto>>
    {
        private readonly IHotelRepository _hotelRepository;
        private readonly IMapper _mapper;

        public SearchForHotelsHandler(IHotelRepository hotelRepository, IMapper mapper)
        {
            _hotelRepository = hotelRepository;
            _mapper = mapper;
        }

        public async Task<PaginatedResult<HotelSearchResultResponseDto>> Handle(SearchHotelsQuery request, CancellationToken cancellationToken)
        {
            var filter = BuildSearchExpression(request);

            var query = new PaginatedQuery<Hotel>(
                filter,
                request.SortColumn,
                request.PageNumber,
                request.PageSize,
                request.OrderDirection ?? OrderDirection.Ascending
            );

            var hotels = await _hotelRepository.FindHotelsAsync(query);
            return _mapper.Map<PaginatedResult<HotelSearchResultResponseDto>>(hotels);
        }

        private Expression<Func<Hotel, bool>> BuildSearchExpression(SearchHotelsQuery request)
        {
            Expression<Func<Hotel, bool>> filter = h => true;

            if (!string.IsNullOrWhiteSpace(request.Search))
            {
                filter = h =>
                    h.Name.Contains(request.Search) ||
                    h.City.Name.Contains(request.Search) ||
                    h.City.Country.Contains(request.Search);
            }

            if (!string.IsNullOrWhiteSpace(request.CityName))
            {
                 filter = CombineExpressions(filter, h => h.City.Name.Contains(request.CityName));
            }

            if (request.MinStarRating.HasValue)
            {
                filter = CombineExpressions(filter, h => h.StarRating >= request.MinStarRating);
            }

            if (request.MinPrice.HasValue)
            {
                filter = CombineExpressions(filter, h => h.RoomClasses.Any(rc => rc.NightlyRate >= request.MinPrice));
            }

            if (request.MaxPrice.HasValue)
            {
                filter = CombineExpressions(filter, h => h.RoomClasses.Any(rc => rc.NightlyRate <= request.MaxPrice));
            }

            if (request.RoomTypes.Any())
            {
                filter = CombineExpressions(filter, h => h.RoomClasses.Any(rc => request.RoomTypes.Contains(rc.TypeOfRoom)));
            }

            filter = CombineExpressions(filter, h =>
                h.RoomClasses.Any(rc =>
                    rc.MaxAdultsCapacity >= request.MaxAdultsCapacity &&
                    rc.MaxChildrenCapacity >= request.MaxChildrenCapacity &&
                    rc.Rooms.Count(r => !r.Bookings.Any(b => b.CheckOutDate > request.CheckInDate && b.CheckInDate < request.CheckOutDate)) >= request.NumberOfRooms
                ));

            return filter;
        }

        private Expression<Func<Hotel, bool>> CombineExpressions(Expression<Func<Hotel, bool>> first, Expression<Func<Hotel, bool>> second)
        {
            var parameter = Expression.Parameter(typeof(Hotel));

            var combined = Expression.AndAlso(
                Expression.Invoke(first, parameter),
                Expression.Invoke(second, parameter)
            );

            return Expression.Lambda<Func<Hotel, bool>>(combined, parameter);
        }
    }
}
