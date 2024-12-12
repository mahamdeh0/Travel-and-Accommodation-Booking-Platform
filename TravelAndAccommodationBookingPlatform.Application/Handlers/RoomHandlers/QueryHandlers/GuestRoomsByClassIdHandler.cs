using AutoMapper;
using MediatR;
using TravelAndAccommodationBookingPlatform.Application.DTOs.RoomDtos;
using TravelAndAccommodationBookingPlatform.Application.Queries.RoomQueries;
using TravelAndAccommodationBookingPlatform.Core.Interfaces.Repositories;
using TravelAndAccommodationBookingPlatform.Core.Models;

namespace TravelAndAccommodationBookingPlatform.Application.Handlers.RoomHandlers.QueryHandlers
{
    public class GuestRoomsByClassIdHandler : IRequestHandler<GuestRoomsByClassIdQuery, PaginatedResult<RoomGuestResponseDto>>
    {
        private readonly IRoomClassRepository _roomClassRepository;
        private readonly IRoomRepository _roomRepository;
        private readonly IMapper _mapper;

        public GuestRoomsByClassIdHandler(IRoomClassRepository roomClassRepository, IRoomRepository roomRepository, IMapper mapper)
        {
            _roomClassRepository = roomClassRepository;
            _roomRepository = roomRepository;
            _mapper = mapper;
        }

        public Task<PaginatedResult<RoomGuestResponseDto>> Handle(GuestRoomsByClassIdQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
