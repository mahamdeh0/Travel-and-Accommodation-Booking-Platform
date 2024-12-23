using TravelAndAccommodationBookingPlatform.Core.Enums;

namespace TravelAndAccommodationBookingPlatform.Core.Entities
{
    public class RoomClass : EntityBase, IAuditableEntity
    {
        public Guid HotelId { get; set; }
        public Hotel Hotel { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public decimal NightlyRate { get; set; }
        public RoomType TypeOfRoom { get; set; }
        public int MaxChildrenCapacity { get; set; }
        public int MaxAdultsCapacity { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public ICollection<Room> Rooms { get; set; } = new List<Room>();
        public ICollection<Image> Gallery { get; set; } = new List<Image>();
        public ICollection<Discount> Discounts { get; set; } = new List<Discount>();
    }
}
