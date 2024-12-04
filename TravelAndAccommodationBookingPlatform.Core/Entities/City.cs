
namespace TravelAndAccommodationBookingPlatform.Core.Entities
{
    public class City : EntityBase, IAuditableEntity
    {
        public Image? SmallPreview { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public string PostOffice { get; set; }
        public string Region { get; set; }
        public ICollection<Hotel> Hotels { get; set; } = new List<Hotel>();
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
