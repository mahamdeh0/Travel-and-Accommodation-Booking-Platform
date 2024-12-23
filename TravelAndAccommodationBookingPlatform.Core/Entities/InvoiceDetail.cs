namespace TravelAndAccommodationBookingPlatform.Core.Entities
{
    public class InvoiceDetail
    {
        public Guid RoomId { get; set; }
        public Guid BookingId { get; set; }
        public Booking Booking { get; set; }
        public string RoomClassName { get; set; }
        public string RoomNumber { get; set; }
        public decimal? DiscountAppliedAtBooking { get; set; }
        public decimal PriceAtReservation { get; set; }
        public decimal? AmountAfterDiscount { get; set; }
        public decimal? TaxAmount { get; set; }
        public decimal? AdditionalCharges { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
