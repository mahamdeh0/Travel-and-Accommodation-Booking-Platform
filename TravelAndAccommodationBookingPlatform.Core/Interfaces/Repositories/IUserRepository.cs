using TravelAndAccommodationBookingPlatform.Core.Entities;

namespace TravelAndAccommodationBookingPlatform.Core.Interfaces.Repositories
{
    /// <summary>
    /// Interface for managing user-related persistence operations.
    /// </summary>
    public interface IUserRepository
    {
        Task<User?> AuthenticateUserAsync(string email, string password);
        Task AddUserAsync(User user);
        Task<User?> GetUserByIdAsync(Guid id);
        Task<bool> UserExistsByEmailAsync(string email);
    }
}
