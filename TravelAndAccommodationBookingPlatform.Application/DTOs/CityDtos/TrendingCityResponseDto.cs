namespace TravelAndAccommodationBookingPlatform.Application.DTOs.CityDtos
{
    public class TrendingCityResponseDto
    {
        public Guid Id { get; init; }
        public string? Thumbnail { get; init; }
        public string Name { get; init; }
        public string Country { get; init; }
    }
}
