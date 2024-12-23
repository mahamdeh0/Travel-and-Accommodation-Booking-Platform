using MediatR;
using TravelAndAccommodationBookingPlatform.Application.DTOs.BookingDtos;

namespace TravelAndAccommodationBookingPlatform.Application.Queries.BookingQueries
{
    public class GetBookingByIdQuery : IRequest<BookingResponseDto>
    {
        public Guid BookingId { get; init; }
    }
}
