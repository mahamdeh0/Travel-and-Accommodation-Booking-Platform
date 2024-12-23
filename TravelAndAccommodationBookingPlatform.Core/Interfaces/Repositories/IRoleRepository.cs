using TravelAndAccommodationBookingPlatform.Core.Entities;

namespace TravelAndAccommodationBookingPlatform.Core.Interfaces.Repositories
{
    /// <summary>
    /// Interface for managing Role-related persistence operations.
    /// </summary>
    public interface IRoleRepository
    {
        Task<Role?> GetRoleByNameAsync(string name);

    }
}
