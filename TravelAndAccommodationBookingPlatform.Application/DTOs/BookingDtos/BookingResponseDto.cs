namespace TravelAndAccommodationBookingPlatform.Application.DTOs.BookingDtos
{
    public class BookingResponseDto
    {
        public Guid Id { get; init; }
        public string HotelName { get; init; }
        public decimal TotalPrice { get; init; }
        public string? GuestRemarks { get; set; }
        public DateOnly CheckInDate { get; init; }
        public DateOnly CheckOutDate { get; init; }
        public DateOnly BookingDate { get; init; }
    }
}
