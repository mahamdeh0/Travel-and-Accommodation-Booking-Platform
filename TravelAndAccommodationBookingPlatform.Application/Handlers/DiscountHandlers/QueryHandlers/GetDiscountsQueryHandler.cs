using AutoMapper;
using MediatR;
using TravelAndAccommodationBookingPlatform.Application.DTOs.DiscountDtos;
using TravelAndAccommodationBookingPlatform.Application.Queries.DiscountQueries;
using TravelAndAccommodationBookingPlatform.Core.DomainMessages;
using TravelAndAccommodationBookingPlatform.Core.Entities;
using TravelAndAccommodationBookingPlatform.Core.Enums;
using TravelAndAccommodationBookingPlatform.Core.Exceptions;
using TravelAndAccommodationBookingPlatform.Core.Interfaces.Repositories;
using TravelAndAccommodationBookingPlatform.Core.Models;

namespace TravelAndAccommodationBookingPlatform.Application.Handlers.DiscountHandlers.QueryHandlers
{
    public class GetDiscountsQueryHandler : IRequestHandler<GetDiscountsQuery, PaginatedResult<DiscountResponseDto>>
    {
        private readonly IDiscountRepository _discountRepository;
        private readonly IRoomClassRepository _roomClassRepository;
        private readonly IMapper _mapper;

        public GetDiscountsQueryHandler(IDiscountRepository discountRepository, IRoomClassRepository roomClassRepository, IMapper mapper)
        {
            _discountRepository = discountRepository;
            _roomClassRepository = roomClassRepository;
            _mapper = mapper;
        }

        public async Task<PaginatedResult<DiscountResponseDto>> Handle(GetDiscountsQuery request, CancellationToken cancellationToken)
        {
            if (!await _roomClassRepository.ExistsAsync(rc => rc.Id == request.RoomClassId))
                throw new NotFoundException(RoomClassMessages.RoomClassNotFound);

            var query = new PaginatedQuery<Discount>(
                d => d.RoomClassId == request.RoomClassId,
                request.SortColumn,
                request.PageNumber,
                request.PageSize,
                request.OrderDirection ?? OrderDirection.Ascending
            );

            var discounts = await _discountRepository.GetDiscountsAsync(query);

            return _mapper.Map<PaginatedResult<DiscountResponseDto>>(discounts);
        }
    }
}
