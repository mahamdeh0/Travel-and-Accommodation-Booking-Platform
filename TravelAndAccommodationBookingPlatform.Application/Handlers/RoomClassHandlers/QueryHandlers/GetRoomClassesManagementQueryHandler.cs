using AutoMapper;
using MediatR;
using TravelAndAccommodationBookingPlatform.Application.DTOs.RoomClassDtos;
using TravelAndAccommodationBookingPlatform.Application.Queries.RoomClassQueries;
using TravelAndAccommodationBookingPlatform.Core.Entities;
using TravelAndAccommodationBookingPlatform.Core.Enums;
using TravelAndAccommodationBookingPlatform.Core.Interfaces.Repositories;
using TravelAndAccommodationBookingPlatform.Core.Models;

namespace TravelAndAccommodationBookingPlatform.Application.Handlers.RoomClassHandlers.QueryHandlers
{
    public class GetRoomClassesManagementQueryHandler : IRequestHandler<GetRoomClassesManagementQuery, PaginatedResult<RoomClassManagementResponseDto>>
    {
        private readonly IRoomClassRepository _roomClassRepository;
        private readonly IDiscountRepository _discountRepository;
        private readonly IMapper _mapper;

        public GetRoomClassesManagementQueryHandler(IRoomClassRepository roomClassRepository, IDiscountRepository discountRepository, IMapper mapper)
        {
            _roomClassRepository = roomClassRepository;
            _discountRepository = discountRepository;
            _mapper = mapper;
        }

        public async Task<PaginatedResult<RoomClassManagementResponseDto>> Handle(GetRoomClassesManagementQuery request, CancellationToken cancellationToken)
        {
            var query = new PaginatedQuery<RoomClass>(
                string.IsNullOrEmpty(request.Search)
                ? _ => true : rc => rc.Name.Contains(request.Search)
                || (rc.Description != null && rc.Description.Contains(request.Search)),
                request.SortColumn,
                request.PageNumber,
                request.PageSize,
                request.OrderDirection ?? OrderDirection.Ascending
            );

            var roomClasses = await _roomClassRepository.GetRoomClassesAsync(query);

            return _mapper.Map<PaginatedResult<RoomClassManagementResponseDto>>(roomClasses);
        }
    }
}
