using MediatR;
using TravelAndAccommodationBookingPlatform.Application.DTOs.BookingDtos;
using TravelAndAccommodationBookingPlatform.Core.Enums;

namespace TravelAndAccommodationBookingPlatform.Application.Commands.BookingCommands
{
    public class CreateBookingCommand : IRequest<BookingResponseDto>
    {
        public IEnumerable<Guid> RoomId { get; init; }
        public Guid HotelId { get; init; }
        public string? GuestRemarks { get; init; }
        public PaymentType PaymentType { get; init; }
        public DateOnly CheckInDate { get; init; }
        public DateOnly CheckOutDate { get; init; }
    }
}
