namespace TravelAndAccommodationBookingPlatform.Core.Exceptions
{
    public abstract class CustomException : Exception
    {
        protected CustomException(string message) : base(message) { }
        public abstract string Title { get; }
        public virtual int ErrorCode => 0;
        public virtual string Details => string.Empty; 
        public override string ToString() => $"{Title}: {Message} (Code: {ErrorCode}) - {Details}";
    }
}
