namespace TravelAndAccommodationBookingPlatform.Core.Exceptions
{
    public class UserWithEmailAlreadyExistsException : ConflictException
    {
        public UserWithEmailAlreadyExistsException(string message) : base(message) { }
        public override string Title => "Email already exists";
        public override string Details => "A user with the same email address already exists in the system. Please use a different email address.";
    }
}
