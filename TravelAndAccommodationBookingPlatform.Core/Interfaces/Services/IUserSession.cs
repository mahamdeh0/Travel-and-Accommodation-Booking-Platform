namespace TravelAndAccommodationBookingPlatform.Core.Interfaces.Services
{
    public interface IUserSession
    {
        Guid GetUserId();
        string GetUserRole();
        string GetUserEmail();
    }
}
