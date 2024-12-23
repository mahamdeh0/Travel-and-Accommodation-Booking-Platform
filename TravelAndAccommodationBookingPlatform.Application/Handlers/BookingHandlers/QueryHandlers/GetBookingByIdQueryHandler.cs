using AutoMapper;
using MediatR;
using TravelAndAccommodationBookingPlatform.Application.DTOs.BookingDtos;
using TravelAndAccommodationBookingPlatform.Application.Queries.BookingQueries;
using TravelAndAccommodationBookingPlatform.Core.DomainMessages;
using TravelAndAccommodationBookingPlatform.Core.Exceptions;
using TravelAndAccommodationBookingPlatform.Core.Interfaces.Repositories;
using TravelAndAccommodationBookingPlatform.Core.Interfaces.Services;

namespace TravelAndAccommodationBookingPlatform.Application.Handlers.BookingHandlers.QueryHandlers
{
    public class GetBookingByIdQueryHandler : IRequestHandler<GetBookingByIdQuery, BookingResponseDto>
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IMapper _mapper;
        private readonly IUserSession _userSession;

        public GetBookingByIdQueryHandler(IBookingRepository bookingRepository,IMapper mapper,IUserSession userSession)
        {
            _bookingRepository = bookingRepository;
            _mapper = mapper;
            _userSession = userSession;
        }

        public async Task<BookingResponseDto> Handle(GetBookingByIdQuery request, CancellationToken cancellationToken)
        {
            var userId = _userSession.GetUserId();
            var role = _userSession.GetUserRole();

            if (role != "Guest")
                throw new ForbiddenException(UserMessages.UserNotGuest);

            var booking = await _bookingRepository.GetBookingByIdAsync(request.BookingId, userId);
            if (booking == null)
                throw new NotFoundException(BookingMessages.BookingNotFound);

            return _mapper.Map<BookingResponseDto>(booking);
        }
    }
}
