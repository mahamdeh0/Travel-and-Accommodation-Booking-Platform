using System.Linq.Expressions;
using TravelAndAccommodationBookingPlatform.Core.Entities;
using TravelAndAccommodationBookingPlatform.Core.Models;

namespace TravelAndAccommodationBookingPlatform.Core.Interfaces.Repositories
{
    /// <summary>
    /// Interface for managing Owner-related persistence operations.
    /// </summary>
    public interface IOwnerRepository
    {
        Task<Owner> AddOwnerAsync(Owner owner);
        Task<PaginatedResult<Owner>> GetOwnersAsync(PaginatedQuery<Owner> query);
        Task UpdateOwnerAsync(Owner owner);
        Task<Owner?> GetOwnerByIdAsync(Guid id);
        Task<bool> OwnerExistsAsync(Expression<Func<Owner, bool>> predicate);
    }
}
