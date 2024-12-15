using AutoMapper;
using MediatR;
using System.Linq.Expressions;
using TravelAndAccommodationBookingPlatform.Application.DTOs.RoomDtos;
using TravelAndAccommodationBookingPlatform.Application.Queries.RoomQueries;
using TravelAndAccommodationBookingPlatform.Core.DomainMessages;
using TravelAndAccommodationBookingPlatform.Core.Entities;
using TravelAndAccommodationBookingPlatform.Core.Exceptions;
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

        public async Task<PaginatedResult<RoomGuestResponseDto>> Handle(GuestRoomsByClassIdQuery request, CancellationToken cancellationToken)
        {
            var roomClassExists = await _roomClassRepository.ExistsAsync(rc => rc.Id == request.RoomClassId);
            if (!roomClassExists)
                throw new NotFoundException(RoomClassMessages.RoomClassNotFound);

            Expression<Func<Room, bool>> filterExpression = room => room.RoomClassId == request.RoomClassId && 
                                                                   !room.Bookings.Any(booking =>
                                                                   booking.CheckInDate < request.CheckOutDate &&
                                                                   booking.CheckOutDate > request.CheckInDate);

            var query = new PaginatedQuery<Room>(
                filterExpression,
                null, 
                request.PageNumber,
                request.PageSize
            );

            var rooms = await _roomRepository.GetRoomsAsync(query);
            return _mapper.Map<PaginatedResult<RoomGuestResponseDto>>(rooms);
        }
    }
}
