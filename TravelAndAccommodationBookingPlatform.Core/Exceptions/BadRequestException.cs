namespace TravelAndAccommodationBookingPlatform.Core.Exceptions
{
    public class BadRequestException : CustomException
    {
        public BadRequestException(string message) : base(message) { }
        public override string Title => "Bad Request";
        public override int ErrorCode => 400;
        public override string Details => "The request could not be understood or was missing required parameters.";
    }
}
