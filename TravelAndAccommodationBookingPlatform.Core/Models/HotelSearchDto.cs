using TravelAndAccommodationBookingPlatform.Core.Enums;

namespace TravelAndAccommodationBookingPlatform.Core.Models
{
    public class HotelSearchDto
    {
        public Guid Id { get; set; }
        public ImageType? Thumbnail { get; set; }
        public string Name { get; set; }
        public int StarRating { get; set; }
        public HotelStatus Status { get; set; }
        public double ReviewsRating { get; set; }
        public decimal StartingPricePerNight { get; set; }
        public string? Description { get; set; }

    }
}
