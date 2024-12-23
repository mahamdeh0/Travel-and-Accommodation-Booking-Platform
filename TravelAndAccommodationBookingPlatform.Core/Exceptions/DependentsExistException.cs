namespace TravelAndAccommodationBookingPlatform.Core.Exceptions
{
    public class DependentsExistException : ConflictException
    {
        public DependentsExistException(string message) : base(message) { }
        public override string Title => "Dependents exist";
        public override string Details => "There are existing dependents associated with this resource, and it cannot be deleted or modified at this time.";
    }
}
