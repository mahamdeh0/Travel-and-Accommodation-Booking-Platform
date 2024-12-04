using System.Linq.Expressions;
using TravelAndAccommodationBookingPlatform.Core.Entities;
using TravelAndAccommodationBookingPlatform.Core.Models;

namespace TravelAndAccommodationBookingPlatform.Core.Interfaces.Repositories
{
    /// <summary>
    /// Interface for managing city-related persistence operations.
    /// </summary>
    internal interface ICityRepository
    {
        Task<City?> GetByIdAsync(Guid id);
        Task<City> AddAsync(City city);
        Task RemoveAsync(Guid id);
        Task UpdateAsync(City city);
        Task<bool> ExistsAsync(Expression<Func<City, bool>> predicate);
        Task<PaginatedResult<CityAdminView>> GetCitiesForAdminAsync(PaginatedQuery<City> PaginatedQuery);
        Task<IEnumerable<City>> GetTopMostVisitedAsync(int count);
    }
}
