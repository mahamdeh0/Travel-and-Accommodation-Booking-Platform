namespace TravelAndAccommodationBookingPlatform.Core.Exceptions
{
    public class NotFoundException : CustomException
    {
        public NotFoundException(string message) : base(message) { }
        public override string Title => "Not Found";
        public override int ErrorCode => 404; 
        public override string Details => "The resource you are looking for was not found.";
    }
}
