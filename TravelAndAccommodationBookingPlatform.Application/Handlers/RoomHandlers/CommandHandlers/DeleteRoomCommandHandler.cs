using MediatR;
using TravelAndAccommodationBookingPlatform.Application.Commands.RoomCommands;
using TravelAndAccommodationBookingPlatform.Core.DomainMessages;
using TravelAndAccommodationBookingPlatform.Core.Exceptions;
using TravelAndAccommodationBookingPlatform.Core.Interfaces.Repositories;
using TravelAndAccommodationBookingPlatform.Core.Interfaces.UnitOfWork;

namespace TravelAndAccommodationBookingPlatform.Application.Handlers.RoomHandlers.CommandHandlers
{
    public class DeleteRoomCommandHandler : IRequestHandler<DeleteRoomCommand>
    {
        private readonly IRoomClassRepository _roomClassRepository;
        private readonly IRoomRepository _roomRepository;
        private readonly IBookingRepository _bookingRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteRoomCommandHandler(IRoomClassRepository roomClassRepository, IRoomRepository roomRepository, IBookingRepository bookingRepository, IUnitOfWork unitOfWork)
        {
            _roomClassRepository = roomClassRepository;
            _roomRepository = roomRepository;
            _bookingRepository = bookingRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(DeleteRoomCommand request, CancellationToken cancellationToken)
        {
            var roomClassExists = await _roomClassRepository.ExistsAsync(rc => rc.Id == request.RoomClassId);
            if (!roomClassExists)
                throw new NotFoundException(RoomClassMessages.RoomClassNotFound);

            var room = await _roomRepository.GetRoomByIdAsync(request.RoomClassId, request.RoomId);
            if (room == null)
                throw new NotFoundException(RoomMessages.RoomNotFound);

            var hasBookings = await _bookingRepository.ExistsByPredicateAsync(b => b.Rooms.Any(r => r.Id == request.RoomId));
            if (hasBookings)
                throw new InvalidOperationException(RoomMessages.CannotDeleteRoom);

            await _roomRepository.RemoveRoomAsync(request.RoomId);
            await _unitOfWork.SaveChangesAsync();

        }
    }
}
