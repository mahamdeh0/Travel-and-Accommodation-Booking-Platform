namespace TravelAndAccommodationBookingPlatform.Core.Exceptions
{
    public class RoomClassWithSameNameFoundException : ConflictException
    {
        public RoomClassWithSameNameFoundException(string message) : base(message) { }
        public override string Title => "Room class with same name exists";
        public override string Details => "Another room class with the same name already exists in the hotel. Please choose a different name for this room class.";
    }
}
