using AutoMapper;
using MediatR;
using TravelAndAccommodationBookingPlatform.Application.Commands.RoomClassCommands;
using TravelAndAccommodationBookingPlatform.Core.DomainMessages;
using TravelAndAccommodationBookingPlatform.Core.Exceptions;
using TravelAndAccommodationBookingPlatform.Core.Interfaces.Repositories;
using TravelAndAccommodationBookingPlatform.Core.Interfaces.UnitOfWork;

namespace TravelAndAccommodationBookingPlatform.Application.Handlers.RoomClassHandlers.CommandHandlers
{
    public class UpdateRoomClassCommandHandler : IRequestHandler<UpdateRoomClassCommand>
    {
        private readonly IRoomClassRepository _roomClassRepository;
        private readonly IHotelRepository _hotelRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateRoomClassCommandHandler(IRoomClassRepository roomClassRepository, IHotelRepository hotelRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _roomClassRepository = roomClassRepository;
            _hotelRepository = hotelRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task Handle(UpdateRoomClassCommand request, CancellationToken cancellationToken)
        {
            var roomClass = await _roomClassRepository.GetRoomClassByIdAsync(request.HotelId);
            if (roomClass == null)
                throw new NotFoundException(RoomClassMessages.RoomClassNotFound);

            if (await _roomClassRepository.ExistsAsync(rc => rc.HotelId == request.HotelId && rc.Name == request.Name))
                throw new RoomClassWithSameNameFoundException(RoomClassMessages.RoomClassNameExistsInHotel);

            _mapper.Map(request, roomClass);

            await _roomClassRepository.UpdateRoomClassAsync(roomClass);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
