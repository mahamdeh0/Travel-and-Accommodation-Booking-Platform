namespace TravelAndAccommodationBookingPlatform.Core.Exceptions
{
    public class UnauthorizedException : CustomException
    {
        public UnauthorizedException(string message) : base(message) { }
        public override string Title => "Unauthorized";
        public override string Details => "You are not authorized to perform this action. Please check your credentials and permissions.";
    }
}
