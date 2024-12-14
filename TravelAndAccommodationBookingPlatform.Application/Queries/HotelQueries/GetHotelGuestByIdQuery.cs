using MediatR;
using TravelAndAccommodationBookingPlatform.Application.DTOs.HotelDtos;

namespace TravelAndAccommodationBookingPlatform.Application.Queries.HotelQueries
{
    public class GetHotelGuestByIdQuery : IRequest<HotelGuestResponseDto>
    {
        public Guid HotelId { get; init; }
    }
}
