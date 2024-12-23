namespace TravelAndAccommodationBookingPlatform.Application.DTOs.CityDtos
{
    public class CityManagementResponseDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int TotalHotels { get; set; }
        public string Country { get; set; }
        public string PostOffice { get; set; }
        public string Region { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
