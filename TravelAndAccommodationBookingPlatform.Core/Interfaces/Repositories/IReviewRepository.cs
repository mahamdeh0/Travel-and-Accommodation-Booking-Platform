using System.Linq.Expressions;
using TravelAndAccommodationBookingPlatform.Core.Entities;
using TravelAndAccommodationBookingPlatform.Core.Models;

namespace TravelAndAccommodationBookingPlatform.Core.Interfaces.Repositories
{
    public interface IReviewRepository
    {
        Task<Review> AddReviewAsync(Review review);
        Task<PaginatedResult<Review>> GetReviewsAsync(PaginatedQuery<Review> query);
        Task<Review?> GetReviewByIdAsync(Guid hotelId, Guid reviewId);
        Task<Review?> GetReviewByIdAsync(Guid reviewId, Guid hotelId, Guid guestId);
        Task<int> GetHotelRatingAsync(Guid hotelId);
        Task<int> GetHotelReviewCountAsync(Guid hotelId);
        Task RemoveReviewAsync(Guid reviewId);
        Task UpdateReviewAsync(Review review);
        Task<bool> ExistsAsync(Expression<Func<Review, bool>> predicate);
    }
}
