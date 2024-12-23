using System.Linq.Expressions;
using TravelAndAccommodationBookingPlatform.Core.Entities;
using TravelAndAccommodationBookingPlatform.Core.Models;

namespace TravelAndAccommodationBookingPlatform.Core.Interfaces.Repositories
{
    /// <summary>
    /// Interface for managing Hotel-related persistence operations.
    /// </summary>
    public interface IHotelRepository
    {
        Task<Hotel> AddHotelAsync(Hotel hotel);
        Task RemoveHotelAsync(Guid id);
        Task UpdateHotelAsync(Hotel hotel);
        Task UpdateReviewById(Guid id, double Rating);
        Task<Hotel?> GetHotelByIdAsync(Guid id);
        Task<PaginatedResult<HotelManagementDto>> GetHotelsForManagementPageAsync(PaginatedQuery<Hotel> query);
        Task<bool> ExistsByPredicateAsync(Expression<Func<Hotel, bool>> predicate);
        Task<PaginatedResult<HotelSearchDto>> FindHotelsAsync(PaginatedQuery<Hotel> query);
    }
}
