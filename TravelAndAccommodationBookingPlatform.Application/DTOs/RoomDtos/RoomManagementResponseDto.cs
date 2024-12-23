namespace TravelAndAccommodationBookingPlatform.Application.DTOs.RoomDtos
{
    public class RoomManagementResponseDto
    {
        public Guid Id { get; init; }
        public Guid RoomClassId { get; init; }
        public bool IsAvailable { get; init; }
        public string Number { get; init; }
        public DateTime CreatedAt { get; init; }
        public DateTime? UpdatedAt { get; init; }
    }
}
