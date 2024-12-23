namespace TravelAndAccommodationBookingPlatform.Core.Exceptions
{
    public class DiscountDateConflictException : ConflictException
    {
        public DiscountDateConflictException(string message) : base(message) { }
        public override string Title => "Discount intervals conflict";
        public override string Details => "The discount activation dates overlap with existing ones, leading to a conflict in the schedule.";
    }
}
