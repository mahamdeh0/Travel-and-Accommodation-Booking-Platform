using AutoMapper;
using MediatR;
using TravelAndAccommodationBookingPlatform.Application.DTOs.DiscountDtos;
using TravelAndAccommodationBookingPlatform.Application.Queries.DiscountQueries;
using TravelAndAccommodationBookingPlatform.Core.DomainMessages;
using TravelAndAccommodationBookingPlatform.Core.Exceptions;
using TravelAndAccommodationBookingPlatform.Core.Interfaces.Repositories;

namespace TravelAndAccommodationBookingPlatform.Application.Handlers.DiscountHandlers.QueryHandlers
{
    public class GetDiscountByIdQueryHandler : IRequestHandler<GetDiscountByIdQuery, DiscountResponseDto>
    {
        private readonly IRoomClassRepository _roomClassRepository;
        private readonly IDiscountRepository _discountRepository;
        private readonly IMapper _mapper;

        public GetDiscountByIdQueryHandler(IRoomClassRepository roomClassRepository, IDiscountRepository discountRepository, IMapper mapper)
        {
            _roomClassRepository = roomClassRepository;
            _discountRepository = discountRepository;
            _mapper = mapper;
        }

        public async Task<DiscountResponseDto> Handle(GetDiscountByIdQuery request, CancellationToken cancellationToken)
        {
            if (!await _roomClassRepository.ExistsAsync(rc => rc.Id == request.RoomClassId))
                throw new NotFoundException(RoomClassMessages.RoomClassNotFound);

            var discount = await _discountRepository.GetDiscountByIdAsync(request.DiscountId, request.RoomClassId);
            if (discount == null)
                throw new NotFoundException(DiscountMessages.DiscountNotFound);

            return _mapper.Map<DiscountResponseDto>(discount);
        }
    }
}
