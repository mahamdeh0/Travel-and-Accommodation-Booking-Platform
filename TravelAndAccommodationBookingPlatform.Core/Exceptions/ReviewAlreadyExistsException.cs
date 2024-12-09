namespace TravelAndAccommodationBookingPlatform.Core.Exceptions
{
    public class ReviewAlreadyExistsException : ConflictException
    {
        public ReviewAlreadyExistsException(string message) : base(message) { }
        public override string Title => "Review already exists";
        public override string Details => "This hotel has already been reviewed. Multiple reviews are not allowed for the same hotel.";
    }
}
