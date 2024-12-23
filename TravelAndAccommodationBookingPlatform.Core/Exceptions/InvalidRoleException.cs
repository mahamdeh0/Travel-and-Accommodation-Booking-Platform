namespace TravelAndAccommodationBookingPlatform.Core.Exceptions
{
    public class InvalidRoleException : BadRequestException
    {
        public InvalidRoleException(string message) : base(message) { }
        public override string Title => "Invalid role";
        public override string Details => "The specified role is invalid. Please provide a valid role for the user.";
    }
}
