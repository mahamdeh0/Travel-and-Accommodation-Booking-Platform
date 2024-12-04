namespace TravelAndAccommodationBookingPlatform.Core.Entities
{
    public class Discount : EntityBase
    {
        public Guid RoomClassId { get; set; }
        public RoomClass RoomClass { get; set; }
        public decimal Percentage { get; set; }
        public string? Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
