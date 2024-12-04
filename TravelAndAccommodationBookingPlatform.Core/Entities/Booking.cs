using TravelAndAccommodationBookingPlatform.Core.Enums;

namespace TravelAndAccommodationBookingPlatform.Core.Entities
{
    public class Booking : EntityBase
    {
        public Guid GuestId { get; set; }
        public Guid HotelId { get; set; }
        public User Guest { get; set; }
        public Hotel Hotel { get; set; }
        public decimal TotalPrice { get; set; }
        public DateOnly CheckInDate { get; set; }
        public DateOnly CheckOutDate { get; set; }
        public DateOnly BookingDate { get; set; }
        public string? GuestRemarks { get; set; }
        public PaymentType PaymentType { get; set; }
        public ICollection<Room> Rooms { get; set; } = new List<Room>();
        public ICollection<InvoiceDetail> Invoice { get; set; } = new List<InvoiceDetail>();
    }
}
