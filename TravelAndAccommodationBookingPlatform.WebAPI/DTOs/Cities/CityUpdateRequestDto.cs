namespace TravelAndAccommodationBookingPlatform.WebAPI.DTOs.Cities
{
    public class CityUpdateRequestDto
    {
        public string Name { get; init; }
        public string Country { get; init; }
        public string Region { get; set; }
        public string PostOffice { get; init; }
    }
}
