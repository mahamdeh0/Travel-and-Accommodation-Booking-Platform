namespace TravelAndAccommodationBookingPlatform.Application.DTOs.DiscountDtos
{
    public class DiscountResponseDto
    {
        public Guid Id { get; init; }
        public Guid RoomClassId { get; init; }
        public DateTime StartDate { get; init; }
        public DateTime EndDate { get; init; }
        public DateTime CreatedAt { get; init; }
        public float Percentage { get; init; }
    }
}
