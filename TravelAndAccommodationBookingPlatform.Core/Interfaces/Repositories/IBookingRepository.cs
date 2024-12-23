using System.Linq.Expressions;
using TravelAndAccommodationBookingPlatform.Core.Entities;
using TravelAndAccommodationBookingPlatform.Core.Models;

namespace TravelAndAccommodationBookingPlatform.Core.Interfaces.Repositories
{
    /// <summary>
    /// Interface for managing Booking-related persistence operations.
    /// </summary>
    public interface IBookingRepository
    {
        Task<Booking> AddBookingAsync(Booking booking);

        Task RemoveBookingAsync(Guid id);

        Task<Booking?> GetBookingByIdAsync(Guid id);

        Task<Booking?> GetBookingByIdAsync(Guid id, Guid guestId);

        Task<PaginatedResult<Booking>> GetBookingsAsync(PaginatedQuery<Booking> query);

        Task<IEnumerable<Booking>> GetRecentBookingsByGuestIdAsync(Guid guestId, int count);
        Task<bool> ExistsByPredicateAsync(Expression<Func<Booking, bool>> predicate);

    }
}
