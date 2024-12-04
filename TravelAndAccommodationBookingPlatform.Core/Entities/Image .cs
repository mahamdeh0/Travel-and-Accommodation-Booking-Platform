using TravelAndAccommodationBookingPlatform.Core.Enums;

namespace TravelAndAccommodationBookingPlatform.Core.Entities
{
    public class Image : EntityBase
    {
        public Guid EntityId { get; set; }
        public ImageType Type { get; set; }
        public string Path { get; set; }
    }
}
