namespace TravelAndAccommodationBookingPlatform.Infrastructure.RateLimiting
{
    public class RateLimiterOptions
    {
        public int MaxRequests { get; set; } = 100;
        public int WindowDurationInSeconds { get; set; } = 60;
    }
}
