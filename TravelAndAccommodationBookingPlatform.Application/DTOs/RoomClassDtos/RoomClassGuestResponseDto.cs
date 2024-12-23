using TravelAndAccommodationBookingPlatform.Application.DTOs.DiscountDtos;
using TravelAndAccommodationBookingPlatform.Core.Enums;

namespace TravelAndAccommodationBookingPlatform.Application.DTOs.RoomClassDtos
{
    public class RoomClassGuestResponseDto
    {
        public Guid RoomClassId { get; init; }
        public string Name { get; init; }
        public string? Description { get; init; }
        public decimal NightlyRate { get; init; }
        public RoomType TypeOfRoom { get; init; }
        public int MaxChildrenCapacity { get; init; }
        public int MaxAdultsCapacity { get; init; }
        public IEnumerable<string> Gallery { get; init; }
        public DiscountResponseDto? ActiveDiscount { get; init; }
    }
}
