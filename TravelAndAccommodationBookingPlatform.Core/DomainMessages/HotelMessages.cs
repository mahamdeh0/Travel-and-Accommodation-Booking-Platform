namespace TravelAndAccommodationBookingPlatform.Core.DomainMessages
{
    public static class HotelMessages
    {
        public const string HotelNotFound = "The hotel with the specified ID could not be found.";
        public const string HotelHasDependents = "The specified hotel still has dependent entities associated with it. Please remove them first.";
        public const string InvalidStarRating = "The star rating must be between 1 and 5.";
        public const string HotelWithSameLocationExists = "A hotel already exists at the same location (longitude, latitude).";
        public const string HotelClosed = "The hotel is currently closed and cannot accept new bookings.";
    }
}
