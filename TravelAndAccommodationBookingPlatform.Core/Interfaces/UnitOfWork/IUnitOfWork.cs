namespace TravelAndAccommodationBookingPlatform.Core.Interfaces.UnitOfWork
{
    public interface IUnitOfWork
    {
        Task<int> SaveChangesAsync();
    }
}
