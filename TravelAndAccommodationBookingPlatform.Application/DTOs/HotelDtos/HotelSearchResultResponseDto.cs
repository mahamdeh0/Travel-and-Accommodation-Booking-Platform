namespace TravelAndAccommodationBookingPlatform.Application.DTOs.HotelDtos
{
    public class HotelSearchResultResponseDto
    {
        public Guid Id { get; init; }
        public string Name { get; init; }
        public string? CityName { get; init; }
        public string? BriefDescription { get; init; }
        public double ReviewsRating { get; init; }
        public int StarRating { get; init; }
        public decimal NightlyRate { get; init; }
        public string? Thumbnail { get; init; }
    }
}
