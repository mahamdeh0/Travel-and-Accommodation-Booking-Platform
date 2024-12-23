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
    public class GetHotelsManagementQueryHandler : IRequestHandler<GetHotelManagementQuery, PaginatedResult<HotelManagementResponseDto>>
    {
        private readonly IHotelRepository _hotelRepository;
        private readonly IMapper _mapper;

        public GetHotelsManagementQueryHandler(IHotelRepository hotelRepository, IMapper mapper)
        {
            _hotelRepository = hotelRepository;
            _mapper = mapper;
        }

        public async Task<PaginatedResult<HotelManagementResponseDto>> Handle(GetHotelManagementQuery request, CancellationToken cancellationToken)
        {
            Expression<Func<Hotel, bool>> filterExpression = string.IsNullOrEmpty(request.Search)
                ? _ => true : hotel => hotel.Name.Contains(request.Search);

            var query = new PaginatedQuery<Hotel>(
                filterExpression,
                request.SortColumn,
                request.PageNumber,
                request.PageSize,
                request.OrderDirection ?? OrderDirection.Ascending
            );

            var hotels = await _hotelRepository.GetHotelsForManagementPageAsync(query);
            return _mapper.Map<PaginatedResult<HotelManagementResponseDto>>(hotels);
        }
    }
}
