namespace TravelAndAccommodationBookingPlatform.WebAPI.DTOs.Discounts
{
    public class DiscountUpdateRequestDto
    {
        public decimal Percentage { get; init; }
        public DateTime StartDate { get; init; }
        public DateTime EndDate { get; init; }
    }
}
