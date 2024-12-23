namespace TravelAndAccommodationBookingPlatform.Core.Exceptions
{
    public class CredentialsNotValidException : UnauthorizedException
    {
        public CredentialsNotValidException(string message) : base(message) { }
        public override string Title => "Invalid credentials";
        public override string Details => "The credentials provided are invalid. Please check the username and password.";
    }
}
