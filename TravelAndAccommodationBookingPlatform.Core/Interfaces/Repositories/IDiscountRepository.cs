using System.Linq.Expressions;
using TravelAndAccommodationBookingPlatform.Core.Entities;
using TravelAndAccommodationBookingPlatform.Core.Models;

namespace TravelAndAccommodationBookingPlatform.Core.Interfaces.Repositories
{
    public interface IDiscountRepository
    {
        Task<Discount> AddDiscountAsync(Discount discount);
        Task<PaginatedResult<Discount>> GetDiscountsAsync(PaginatedQuery<Discount> query);
        Task<Discount?> GetDiscountByIdAsync(Guid id);
        Task RemoveDiscountAsync(Guid id);
        Task<bool> ExistsAsync(Expression<Func<Discount, bool>> predicate);
    }
}
