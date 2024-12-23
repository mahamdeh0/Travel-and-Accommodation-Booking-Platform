namespace TravelAndAccommodationBookingPlatform.Core.Exceptions
{
    public class ForbiddenException : CustomException
    {
        public ForbiddenException(string message) : base(message) { }
        public override string Title => "Forbidden";
        public override string Details => "You do not have the necessary permissions to access this resource.";
    }
}
