namespace TravelAndAccommodationBookingPlatform.Core.Exceptions
{
    public class RoomNotAvailableException : BadRequestException
    {
        public RoomNotAvailableException(string message) : base(message) { }
        public override string Title => "Room not available";
        public override string Details => "The requested room is not available for booking at the moment. Please try again later or choose another room.";
    }
}
