namespace TravelAndAccommodationBookingPlatform.Core.Exceptions
{
    public class RoomsNotInHotelException : BadRequestException
    {
        public RoomsNotInHotelException(string message) : base(message) { }
        public override string Title => "Rooms not found in the hotel";
        public override string Details => "The specified rooms do not exist in this hotel. Please verify the room IDs and try again.";
    }
}
