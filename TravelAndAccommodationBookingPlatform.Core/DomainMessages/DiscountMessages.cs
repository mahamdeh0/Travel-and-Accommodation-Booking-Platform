namespace TravelAndAccommodationBookingPlatform.Core.DomainMessages
{
    public static class DiscountMessages
    {
        public const string DiscountNotFound = "The discount with the specified ID was not found.";
        public const string InvalidDiscountPercentage = "The discount percentage must be between 0 and 100.";
        public const string DiscountNotFoundInRoomClass = "No discount with the specified ID exists within the given room class.";
        public const string DiscountExpired = "The specified discount is no longer valid as it has expired.";
        public const string ConflictingDiscountInterval = "A discount already exists within the specified date interval.";
    }
}
