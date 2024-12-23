namespace TravelAndAccommodationBookingPlatform.Core.Exceptions
{
    public class DuplicateHotelLocationException : ConflictException
    {
        public DuplicateHotelLocationException(string message) : base(message) { }
        public override string Title => "Hotel location already exists";
        public override string Details => "Another hotel already exists in the same location. Please choose a different location for this hotel.";

    }
}
