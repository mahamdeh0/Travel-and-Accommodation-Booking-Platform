using AutoMapper;
using MediatR;
using TravelAndAccommodationBookingPlatform.Application.Commands.RoomCommands;
using TravelAndAccommodationBookingPlatform.Core.DomainMessages;
using TravelAndAccommodationBookingPlatform.Core.Exceptions;
using TravelAndAccommodationBookingPlatform.Core.Interfaces.Repositories;
using TravelAndAccommodationBookingPlatform.Core.Interfaces.UnitOfWork;

namespace TravelAndAccommodationBookingPlatform.Application.Handlers.RoomHandlers.CommandHandlers
{
    public class UpdateRoomCommandHandler : IRequestHandler<UpdateRoomCommand>
    {
        private readonly IRoomClassRepository _roomClassRepository;
        private readonly IRoomRepository _roomRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateRoomCommandHandler(IRoomClassRepository roomClassRepository, IRoomRepository roomRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _roomClassRepository = roomClassRepository;
            _roomRepository = roomRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task Handle(UpdateRoomCommand request, CancellationToken cancellationToken)
        {
            var roomClassExists = await _roomClassRepository.ExistsAsync(rc => rc.Id == request.RoomClassId);
            if (!roomClassExists)
                throw new NotFoundException(RoomClassMessages.RoomClassNotFound);

            var room = await _roomRepository.GetRoomByIdAsync(request.RoomClassId, request.RoomId);
            if (room == null)
                throw new NotFoundException(RoomMessages.RoomNotFound);

            _mapper.Map(request, room);

            await _roomRepository.UpdateRoomAsync(room);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
