using TravelAndAccommodationBookingPlatform.Application.DTOs.DiscountDtos;

namespace TravelAndAccommodationBookingPlatform.Application.DTOs.RoomClassDtos
{
    public class RoomClassManagementResponseDto
    {
        public Guid RoomClassId { get; init; }
        public string Name { get; init; }
        public string? Description { get; init; }
        public decimal NightlyRate { get; init; }
        public int MaxChildrenCapacity { get; init; }
        public int MaxAdultsCapacity { get; init; }
        public DiscountResponseDto? ActiveDiscount { get; init; }
        public DateTime CreatedAt { get; init; }
        public DateTime? UpdatedAt { get; init; }
    }
}
