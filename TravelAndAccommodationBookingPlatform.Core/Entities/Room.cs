namespace TravelAndAccommodationBookingPlatform.Core.Entities
{
    public class Room : EntityBase, IAuditableEntity
    {
        public Guid RoomClassId { get; set; }
        public RoomClass RoomClass { get; set; }
        public string Number { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public ICollection<InvoiceDetail> InvoiceDetail { get; set; } = new List<InvoiceDetail>();
        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
    }
}
