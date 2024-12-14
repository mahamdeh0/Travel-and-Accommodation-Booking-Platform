using MediatR;
using TravelAndAccommodationBookingPlatform.Application.Commands.DiscountCommands;
using TravelAndAccommodationBookingPlatform.Core.DomainMessages;
using TravelAndAccommodationBookingPlatform.Core.Exceptions;
using TravelAndAccommodationBookingPlatform.Core.Interfaces.Repositories;
using TravelAndAccommodationBookingPlatform.Core.Interfaces.UnitOfWork;

namespace TravelAndAccommodationBookingPlatform.Application.Handlers.DiscountHandlers.CommandHandlers
{
    public class DeleteDiscountCommandHandler : IRequestHandler<DeleteDiscountCommand>
    {
        private readonly IRoomClassRepository _roomClassRepository;
        private readonly IDiscountRepository _discountRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteDiscountCommandHandler(IRoomClassRepository roomClassRepository, IDiscountRepository discountRepository, IUnitOfWork unitOfWork)
        {
            _roomClassRepository = roomClassRepository;
            _discountRepository = discountRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(DeleteDiscountCommand request, CancellationToken cancellationToken)
        {
            if (!await _roomClassRepository.ExistsAsync(rc => rc.Id == request.RoomClassId))
                throw new NotFoundException(RoomClassMessages.RoomClassNotFound);

            if (!await _discountRepository.ExistsAsync(d => d.Id == request.DiscountId && d.RoomClassId == request.RoomClassId))
                throw new NotFoundException(DiscountMessages.DiscountNotFoundInRoomClass);

            await _discountRepository.RemoveDiscountAsync(request.DiscountId);

            await _unitOfWork.SaveChangesAsync();

        }
    }
}
