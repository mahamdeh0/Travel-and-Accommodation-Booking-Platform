namespace TravelAndAccommodationBookingPlatform.Core.Exceptions
{
    public class RoomWithNumberExistsInRoomClassException : ConflictException
    {
        public RoomWithNumberExistsInRoomClassException(string message) : base(message) { }
        public override string Title => "Room with the same number exists";
        public override string Details => "A room with the same number already exists in the specified room class. Please assign a different number to this room.";
    }
}
