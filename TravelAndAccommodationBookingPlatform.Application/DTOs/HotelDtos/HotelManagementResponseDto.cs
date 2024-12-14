using TravelAndAccommodationBookingPlatform.Application.DTOs.OwnerDtos;

namespace TravelAndAccommodationBookingPlatform.Application.DTOs.HotelDtos
{
    public class HotelManagementResponseDto
    {
        public Guid Id { get; init; }
        public string Name { get; init; }
        public int StarRating { get; init; }
        public int NumberOfRooms { get; init; }
        public DateTime CreatedAt { get; init; }
        public DateTime? UpdatedAt { get; init; }
        public OwnerResponseDto Owner { get; init; }

    }
}
