using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TravelAndAccommodationBookingPlatform.Core.Entities;
using TravelAndAccommodationBookingPlatform.Core.Interfaces.Repositories;
using TravelAndAccommodationBookingPlatform.Core.Models;
using TravelAndAccommodationBookingPlatform.Infrastructure.Data;
using TravelAndAccommodationBookingPlatform.Infrastructure.Extensions;

namespace TravelAndAccommodationBookingPlatform.Infrastructure.Repositories
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly AppDbContext _context;

        public ReviewRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Review> AddReviewAsync(Review review)
        {
            ArgumentNullException.ThrowIfNull(review);

            var newReview = await _context.Reviews.AddAsync(review);

            return newReview.Entity;
        }

        public async Task<PaginatedResult<Review>> GetReviewsAsync(PaginatedQuery<Review> query)
        {
            ArgumentNullException.ThrowIfNull(query);

            var queryable = _context.Reviews.AsQueryable();

            if (query.FilterExpression != null)
                queryable = queryable.Where(query.FilterExpression);

            if (!string.IsNullOrEmpty(query.SortByColumn))
                queryable = queryable.Sort(query.SortByColumn, query.SortDirection);

            var paginationMetadata = await queryable.GetPaginationMetadataAsync(query.PageNumber, query.PageSize);

            var pagedItems = await queryable
                .GetPage(query.PageNumber, query.PageSize)
                .Include(r => r.Guest)
                .Include(r => r.Hotel)
                .AsNoTracking()
                .ToListAsync();

            return new PaginatedResult<Review>(pagedItems, paginationMetadata);
        }

        public async Task<Review?> GetReviewByIdAsync(Guid hotelId, Guid reviewId)
        {
            return await _context.Reviews.FirstOrDefaultAsync(r => r.HotelId == hotelId && r.Id == reviewId);
        }

        public async Task<Review?> GetReviewByIdAsync(Guid reviewId, Guid hotelId, Guid guestId)
        {
            return await _context.Reviews.FirstOrDefaultAsync(r => r.Id == reviewId && r.HotelId == hotelId && r.GuestId == guestId);
        }

        public async Task<int> GetHotelRatingAsync(Guid hotelId)
        {
            var reviews = await _context.Reviews
                .Where(r => r.HotelId == hotelId)
                .ToListAsync();

            if (!reviews.Any()) return 0;

            return (int)reviews.Average(r => r.Rating);
        }

        public async Task<int> GetHotelReviewCountAsync(Guid hotelId)
        {
            return await _context.Reviews.CountAsync(r => r.HotelId == hotelId);
        }

        public async Task RemoveReviewAsync(Guid reviewId)
        {
            var review = await _context.Reviews.FindAsync(reviewId);
            if (review == null) return;

            _context.Reviews.Remove(review);
        }

        public async Task UpdateReviewAsync(Review review)
        {
            ArgumentNullException.ThrowIfNull(review);

            var existingReview = await _context.Reviews.FindAsync(review.Id);
            if (existingReview == null) return;

            _context.Reviews.Update(review);
        }

        public async Task<bool> ExistsAsync(Expression<Func<Review, bool>> predicate)
        {
            return await _context.Reviews.AnyAsync(predicate);
        }
    }
}
