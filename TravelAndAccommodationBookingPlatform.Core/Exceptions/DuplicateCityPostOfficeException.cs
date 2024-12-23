namespace TravelAndAccommodationBookingPlatform.Core.Exceptions
{
    public class DuplicateCityPostOfficeException : ConflictException
    {
        public DuplicateCityPostOfficeException(string message) : base(message) { }

        public override string Title => "City with post office already exists";
        public override string Details => "A city with the specified post office already exists in the system. Please use a different post office or update the existing city.";
    }
}
