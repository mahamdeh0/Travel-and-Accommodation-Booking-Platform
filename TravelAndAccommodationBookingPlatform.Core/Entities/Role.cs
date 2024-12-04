namespace TravelAndAccommodationBookingPlatform.Core.Entities
{
    public class Role : EntityBase
    {
        public string Name { get; set; }
        public string Description { get; set; } 
        public ICollection<User> Users { get; set; } = new List<User>();
    }
}
