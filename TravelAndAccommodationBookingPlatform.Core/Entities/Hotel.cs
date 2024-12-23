using TravelAndAccommodationBookingPlatform.Core.Enums;

namespace TravelAndAccommodationBookingPlatform.Core.Entities
{
    public class Hotel : EntityBase, IAuditableEntity
    {
        public Owner Owner { get; set; }
        public City City { get; set; }
        public Guid CityId { get; set; }
        public Guid OwnerId { get; set; }
        public string Name { get; set; }
        public double ReviewsRating { get; set; }
        public int StarRating { get; set; }
        public string? Website { get; set; }
        public string? BriefDescription { get; set; }
        public string? FullDescription { get; set; }
        public string Geolocation { get; set; }
        public HotelStatus Status { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public Image? SmallPreview { get; set; }
        public ICollection<Image> FullView { get; set; } = new List<Image>();
        public ICollection<RoomClass> RoomClasses { get; set; } = new List<RoomClass>();
        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
        public ICollection<Review> Reviews { get; set; } = new List<Review>();
    }
}
