using TravelAndAccommodationBookingPlatform.Core.Enums;

namespace TravelAndAccommodationBookingPlatform.WebAPI.DTOs.RoomClasses
{
    public class RoomClassUpdateRequestDto
    {
        public string Name { get; init; }
        public int MaxChildrenCapacity { get; init; }
        public int MaxAdultsCapacity { get; init; }
        public RoomType TypeOfRoom { get; init; }
        public decimal NightlyRate { get; init; }
        public string? Description { get; init; }
    }
}
