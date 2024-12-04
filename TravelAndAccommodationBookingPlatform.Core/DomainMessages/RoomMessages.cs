namespace TravelAndAccommodationBookingPlatform.Core.DomainMessages
{
    public static class RoomMessages
    {
        public const string RoomNotFound = "Room with the given ID was not found.";
        public const string DependentsExist = "This room has existing bookings and cannot be modified.";
        public const string RoomsNotBelongToSameHotel = "The specified rooms do not belong to the same hotel.";
        public const string InvalidRoomClass = "The specified room class is invalid or does not exist.";
        public const string InvalidRoomNumber = "The room number is invalid or already exists in the specified room class.";
        public const string CannotDeleteRoom = "The room cannot be deleted as it is associated with existing bookings.";
        public static string RoomUnavailable(Guid roomId)
        {
            return $"The room with ID {roomId} is not available during the selected time period.";
        }

    }
}
