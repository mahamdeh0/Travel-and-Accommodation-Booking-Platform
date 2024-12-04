namespace TravelAndAccommodationBookingPlatform.Core.DomainMessages
{
    public static class CityMessages
    {
        public const string CityNotFound = "No city found with the given ID.";
        public const string CityCreationFailed = "City creation failed. Please try again later.";
        public const string CityHasLinkedEntities = "The city cannot be deleted or modified because it has linked entities (e.g., hotels, bookings). Please remove or update the linked entities first before proceeding.";
        public const string CityUpdatedSuccessfully = "City updated successfully.";
        public const string CityDeletionFailed = "Failed to delete the city. Ensure there are no dependents attached.";
        public const string CityWithPostalCodeExists = "A city with the provided postal code already exists.";
    }
}
