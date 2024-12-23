namespace TravelAndAccommodationBookingPlatform.Application.DTOs.CityDtos
{
    public class CityResponseDto
    {
        public Guid Id { get; init; }
        public string Name { get; init; }
        public string Country { get; init; }
        public string PostOffice { get; init; }
    }
}
