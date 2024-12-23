using AutoMapper;
using MediatR;
using TravelAndAccommodationBookingPlatform.Application.Commands.RoomClassCommands;
using TravelAndAccommodationBookingPlatform.Core.DomainMessages;
using TravelAndAccommodationBookingPlatform.Core.Entities;
using TravelAndAccommodationBookingPlatform.Core.Exceptions;
using TravelAndAccommodationBookingPlatform.Core.Interfaces.Repositories;
using TravelAndAccommodationBookingPlatform.Core.Interfaces.UnitOfWork;

namespace TravelAndAccommodationBookingPlatform.Application.Handlers.RoomClassHandlers.CommandHandlers
{
    public class CreateRoomClassCommandHandler : IRequestHandler<CreateRoomClassCommand, Guid>
    {
        private readonly IRoomClassRepository _roomClassRepository;
        private readonly IHotelRepository _hotelRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateRoomClassCommandHandler(IRoomClassRepository roomClassRepository, IHotelRepository hotelRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _roomClassRepository = roomClassRepository;
            _hotelRepository = hotelRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Guid> Handle(CreateRoomClassCommand request, CancellationToken cancellationToken)
        {
            if (!await _hotelRepository.ExistsByPredicateAsync(h => h.Id == request.HotelId))
                throw new NotFoundException(HotelMessages.HotelNotFound);

            if (await _roomClassRepository.ExistsAsync(rc => rc.HotelId == request.HotelId && rc.Name == request.Name))
                throw new RoomClassWithSameNameFoundException(RoomClassMessages.RoomClassNameExistsInHotel);

            var roomClass = _mapper.Map<RoomClass>(request);

            var createdRoomClass = await _roomClassRepository.AddRoomClassAsync(roomClass);
            await _unitOfWork.SaveChangesAsync();
            return createdRoomClass.Id;
        }
    }
}
