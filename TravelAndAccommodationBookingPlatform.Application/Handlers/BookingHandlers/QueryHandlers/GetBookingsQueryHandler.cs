using AutoMapper;
using MediatR;
using TravelAndAccommodationBookingPlatform.Application.DTOs.BookingDtos;
using TravelAndAccommodationBookingPlatform.Application.Queries.BookingQueries;
using TravelAndAccommodationBookingPlatform.Core.Interfaces.Repositories;
using TravelAndAccommodationBookingPlatform.Core.Interfaces.Services;
using TravelAndAccommodationBookingPlatform.Core.Models;
using TravelAndAccommodationBookingPlatform.Core.Exceptions;
using TravelAndAccommodationBookingPlatform.Core.DomainMessages;
using System.Linq.Expressions;
using TravelAndAccommodationBookingPlatform.Core.Entities;
using TravelAndAccommodationBookingPlatform.Core.Enums;

namespace TravelAndAccommodationBookingPlatform.Application.Handlers.BookingHandlers.QueryHandlers
{
    public class GetBookingsQueryHandler : IRequestHandler<GetBookingsQuery, PaginatedResult<BookingResponseDto>>
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IMapper _mapper;
        private readonly IUserSession _userSession;

        public GetBookingsQueryHandler(
            IBookingRepository bookingRepository,
            IMapper mapper,
            IUserSession userSession)
        {
            _bookingRepository = bookingRepository;
            _mapper = mapper;
            _userSession = userSession;
        }

        public async Task<PaginatedResult<BookingResponseDto>> Handle(GetBookingsQuery request, CancellationToken cancellationToken)
        {
            var userId = _userSession.GetUserId();
            var role = _userSession.GetUserRole();

            if (role != "Guest")
                throw new ForbiddenException(UserMessages.UserNotGuest);

            var filterExpression = (Expression<Func<Booking, bool>>)(b => b.GuestId == userId);
            var paginatedQuery = new PaginatedQuery<Booking>(
                filterExpression,
                request.SortColumn,
                request.PageNumber,
                request.PageSize,
                request.OrderDirection ?? OrderDirection.Ascending);

            var paginatedBookings = await _bookingRepository.GetBookingsAsync(paginatedQuery);

            return _mapper.Map<PaginatedResult<BookingResponseDto>>(paginatedBookings);

        }
    }
}
