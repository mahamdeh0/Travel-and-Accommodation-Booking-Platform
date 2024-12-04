namespace TravelAndAccommodationBookingPlatform.Core.DomainMessages
{
    public static class RoomClassMessages
    {
        public const string RoomClassNotFound = "The room class with the provided ID could not be found.";
        public const string RoomClassNameExistsInHotel = "A room class with the same name already exists in this hotel. Please choose a different name.";
        public const string ExistingRoomsForRoomClass = "There are existing rooms assigned to the specified room class.";
        public const string RoomNotFoundInRoomClass = "The specified room could not be found in the provided room class.";
        public const string DuplicatedRoomNumber = "A room with the same number already exists in the specified room class.";
    }
}
