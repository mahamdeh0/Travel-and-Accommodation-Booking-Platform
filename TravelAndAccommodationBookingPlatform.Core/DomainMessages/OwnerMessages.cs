namespace TravelAndAccommodationBookingPlatform.Core.DomainMessages
{
    public static class OwnerMessages
    {
        public const string OwnerNotFound = "Owner with the provided ID is not found.";
        public const string EmailAlreadyExists = "An owner with the provided email address already exists.";
        public const string InvalidOwnerDetails = "The provided owner details are invalid. Please check the input and try again.";
        public const string DeletionNotAllowed = "The owner cannot be deleted because there are existing hotels or bookings associated with them.";
    }
}
