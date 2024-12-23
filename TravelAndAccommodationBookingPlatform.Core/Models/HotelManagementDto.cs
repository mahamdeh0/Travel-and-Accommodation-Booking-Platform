using TravelAndAccommodationBookingPlatform.Core.Entities;

namespace TravelAndAccommodationBookingPlatform.Core.Models
{
    public class HotelManagementDto
    {
        public Owner Owner { get; set; }
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int StarRating { get; set; }
        public int NumberOfRooms { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
