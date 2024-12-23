using TravelAndAccommodationBookingPlatform.Core.Interfaces.Services;

namespace TravelAndAccommodationBookingPlatform.Infrastructure.Services
{
    public class DateTimeProvider : IDateTimeProvider
    {
        public DateTime Now => DateTime.Now;
    }

}
