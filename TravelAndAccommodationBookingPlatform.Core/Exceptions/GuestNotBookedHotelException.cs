namespace TravelAndAccommodationBookingPlatform.Core.Exceptions
{
    public class GuestNotBookedHotelException : ConflictException
    {
        public GuestNotBookedHotelException(string message) : base(message) { }
        public override string Title => "Guest has not booked a hotel room";
        public override string Details => "The guest has not made any booking for a room at the hotel, so the operation cannot be processed.";

    }
}
