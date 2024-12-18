namespace TravelAndAccommodationBookingPlatform.WebAPI.DTOs.RoomClasses
{
    public class RoomClassCreationRequestDto
    {
        public Guid HotelId { get; init; }
        public string Name { get; init; }
        public int MaxChildrenCapacity { get; init; }
        public int MaxAdultsCapacity { get; init; }
        public decimal NightlyRate { get; init; }
        public string? Description { get; init; }
    }
}
