namespace TravelAndAccommodationBookingPlatform.Infrastructure.Auth
{
    public class JwtSettings
    {
        public required string Key { get; set; }
        public required string Issuer { get; set; }
        public required string Audience { get; set; }
        public required double LifetimeMinutes { get; set; }
    }
}
