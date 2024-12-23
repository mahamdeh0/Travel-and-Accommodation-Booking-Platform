namespace TravelAndAccommodationBookingPlatform.Core.Exceptions
{
    public class CannotCancelBookingException : ConflictException
    {
        public CannotCancelBookingException(string message) : base(message) { }
        public override string Title => "Cannot cancel booking";
        public override string Details => "The booking cannot be canceled because it has already been processed or is no longer eligible for cancellation.";
    }
}
