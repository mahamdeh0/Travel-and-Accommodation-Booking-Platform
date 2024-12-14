namespace TravelAndAccommodationBookingPlatform.Application.DTOs.HotelDtos
{
    public class HotelGuestResponseDto
    {
        public Guid Id { get; init; }
        public string Name { get; init; }
        public string CountryName { get; init; }
        public string CityName { get; init; }
        public string Geolocation { get; init; }
        public int StarRating { get; init; }
        public float ReviewsRating { get; init; }
        public string? BriefDescription { get; init; }
        public string? FullDescription { get; init; }
        public string? Thumbnail { get; init; }
        public IEnumerable<string> Gallery { get; init; }
    }
}
