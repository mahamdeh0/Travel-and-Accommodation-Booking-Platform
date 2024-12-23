using MediatR;
using TravelAndAccommodationBookingPlatform.Application.Commands.BookingCommands;
using TravelAndAccommodationBookingPlatform.Core.DomainMessages;
using TravelAndAccommodationBookingPlatform.Core.Exceptions;
using TravelAndAccommodationBookingPlatform.Core.Interfaces.Repositories;
using TravelAndAccommodationBookingPlatform.Core.Interfaces.Services;
using TravelAndAccommodationBookingPlatform.Core.Interfaces.UnitOfWork;

namespace TravelAndAccommodationBookingPlatform.Application.Handlers.BookingHandlers.CommandHandlers
{
    public class DeleteBookingCommandHandler : IRequestHandler<DeleteBookingCommand>
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserSession _userSession;

        public DeleteBookingCommandHandler(
            IBookingRepository bookingRepository,
            IUnitOfWork unitOfWork,
            IUserSession userSession)
        {
            _bookingRepository = bookingRepository;
            _unitOfWork = unitOfWork;
            _userSession = userSession;
        }

        public async Task Handle(DeleteBookingCommand request, CancellationToken cancellationToken)
        {
            var userId = _userSession.GetUserId();
            var role = _userSession.GetUserRole();

            if (role != "Guest")
                throw new ForbiddenException(UserMessages.UserNotGuest);

            var booking = await _bookingRepository.GetBookingByIdAsync(request.BookingId, userId);
            if (booking == null)
                throw new NotFoundException(BookingMessages.BookingNotFound);

            if (booking.CheckInDate <= DateOnly.FromDateTime(DateTime.UtcNow))
                throw new CannotCancelBookingException(BookingMessages.CannotCancelAfterCheckIn);

            await _bookingRepository.RemoveBookingAsync(request.BookingId);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}