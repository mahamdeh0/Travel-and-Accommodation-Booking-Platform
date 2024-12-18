namespace TravelAndAccommodationBookingPlatform.Application.DTOs.HotelDtos
{
    public class HotelFeaturedDealResponseDto
    {
        public Guid Id { get; init; }
        public string Name { get; init; }
        public string RoomClassName { get; init; }
        public string CityName { get; init; }
        public string Geolocation { get; init; }
        public int StarRating { get; init; }
        public decimal NightlyRate { get; init; }
        public decimal DiscountPercentage { get; init; }
        public DateTime DiscountStartDate { get; init; }
        public DateTime DiscountEndDate { get; init; }
        public float ReviewsRating { get; init; }
        public string? Description { get; init; }
        public string? Thumbnail { get; init; }
    }
}
