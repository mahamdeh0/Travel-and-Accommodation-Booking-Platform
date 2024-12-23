namespace TravelAndAccommodationBookingPlatform.Application.DTOs.CityDtos
{
    public class CityManagementResponseDto
    {
        public Guid Id { get; init; }
        public string Name { get; init; }
        public int TotalHotels { get; init; }
        public string Country { get; init; }
        public string PostOffice { get; init; }
        public string Region { get; init; }
        public DateTime CreatedAt { get; init; }
        public DateTime? UpdatedAt { get; init; }
    }
}
