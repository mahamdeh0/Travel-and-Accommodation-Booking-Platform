using TravelAndAccommodationBookingPlatform.Core.Enums;

namespace TravelAndAccommodationBookingPlatform.WebAPI.DTOs.Bookings
{
    public class BookingCreationRequestDto
    {
        public IEnumerable<Guid> RoomId { get; init; }
        public Guid HotelId { get; init; }
        public string? GuestRemarks { get; init; }
        public PaymentType PaymentMethod { get; init; }
        public DateOnly CheckInDate { get; init; }
        public DateOnly CheckOutDate { get; init; }
    }
}
