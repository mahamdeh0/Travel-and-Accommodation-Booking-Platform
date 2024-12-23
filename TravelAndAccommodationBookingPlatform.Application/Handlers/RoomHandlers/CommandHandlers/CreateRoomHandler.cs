using AutoMapper;
using MediatR;
using TravelAndAccommodationBookingPlatform.Application.Commands.RoomCommands;
using TravelAndAccommodationBookingPlatform.Core.DomainMessages;
using TravelAndAccommodationBookingPlatform.Core.Entities;
using TravelAndAccommodationBookingPlatform.Core.Exceptions;
using TravelAndAccommodationBookingPlatform.Core.Interfaces.Repositories;
using TravelAndAccommodationBookingPlatform.Core.Interfaces.UnitOfWork;

namespace TravelAndAccommodationBookingPlatform.Application.Handlers.RoomHandlers.CommandHandlers
{
    public class CreateRoomHandler : IRequestHandler<CreateRoomCommand, Guid>
    {
        private readonly IMapper _mapper;
        private readonly IRoomClassRepository _roomClassRepository;
        private readonly IRoomRepository _roomRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateRoomHandler(IMapper mapper, IRoomClassRepository roomClassRepository, IRoomRepository roomRepository, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _roomClassRepository = roomClassRepository;
            _roomRepository = roomRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> Handle(CreateRoomCommand request, CancellationToken cancellationToken)
        {
            var roomClassExists = await _roomClassRepository.ExistsAsync(rc => rc.Id == request.RoomClassId);
            if (!roomClassExists)
                throw new NotFoundException(RoomClassMessages.RoomClassNotFound);

            var roomExists = await _roomRepository.ExistsByPredicateAsync(r => r.Number == request.Number && r.RoomClassId == request.RoomClassId);
            if (roomExists)
                throw new RoomWithNumberExistsInRoomClassException(RoomClassMessages.DuplicatedRoomNumber);

            var createdRoom = await _roomRepository.AddRoomAsync(_mapper.Map<Room>(request));

            await _unitOfWork.SaveChangesAsync();
            return createdRoom.Id;
        }
    }
}
