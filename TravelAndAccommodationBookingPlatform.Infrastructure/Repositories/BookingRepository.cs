using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TravelAndAccommodationBookingPlatform.Core.Entities;
using TravelAndAccommodationBookingPlatform.Core.Enums;
using TravelAndAccommodationBookingPlatform.Core.Interfaces.Repositories;
using TravelAndAccommodationBookingPlatform.Core.Models;
using TravelAndAccommodationBookingPlatform.Infrastructure.Data;
using TravelAndAccommodationBookingPlatform.Infrastructure.Extensions;

namespace TravelAndAccommodationBookingPlatform.Infrastructure.Persistence.Repositories
{
    public class BookingRepository : IBookingRepository
    {
        private readonly AppDbContext _context;

        public BookingRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Booking> AddBookingAsync(Booking booking)
        {
            ArgumentNullException.ThrowIfNull(booking);

            var addedBooking = await _context.Bookings.AddAsync(booking);

            return addedBooking.Entity;
        }

        public async Task RemoveBookingAsync(Guid id)
        {
            var booking = await _context.Bookings.FindAsync(id);
            if (booking != null)
                _context.Bookings.Remove(booking);
        }

        public async Task<Booking?> GetBookingByIdAsync(Guid id)
        {
            return await _context.Bookings.FindAsync(id);
        }

        public async Task<Booking?> GetBookingByIdAsync(Guid id, Guid guestId)
        {
            return await _context.Bookings
                .Where(b => b.Id == id && b.GuestId == guestId)
                .Include(h => h.Hotel)
                .FirstOrDefaultAsync();
        }

        public async Task<PaginatedResult<Booking>> GetBookingsAsync(PaginatedQuery<Booking> query)
        {
            var queryable = _context.Bookings.AsQueryable();

            if (query.FilterExpression != null)
                queryable = queryable.Where(query.FilterExpression);

            queryable = queryable.Sort(query.SortByColumn, query.SortDirection);

            var paginationMetadata = await queryable.GetPaginationMetadataAsync(query.PageNumber, query.PageSize);
            var pagedItems = await queryable.GetPage(query.PageNumber, query.PageSize).AsNoTracking().ToListAsync();

            return new PaginatedResult<Booking>(pagedItems, paginationMetadata);
        }

        public async Task<IEnumerable<Booking>> GetRecentBookingsByGuestIdAsync(Guid guestId, int count)
        {
            var topBookingIdsQuery =
                from b in _context.Bookings
                where b.GuestId == guestId
                group b by b.HotelId into g
                let latestBooking = g.OrderByDescending(b => b.CheckInDate).FirstOrDefault()
                where latestBooking != null
                orderby latestBooking.CheckInDate descending
                select latestBooking.Id;

            var topBookingIds = await topBookingIdsQuery
                .Take(count)
                .ToListAsync();

            if (!topBookingIds.Any())
                return Enumerable.Empty<Booking>();

            var recentBookings = await _context.Bookings
                .Where(b => topBookingIds.Contains(b.Id))
                .Include(b => b.Hotel)
                    .ThenInclude(h => h.City)
                .Include(b => b.Hotel.Gallery.Where(img => img.Type == ImageType.Thumbnail))
                .AsNoTracking()
                .ToListAsync();

            return recentBookings.OrderBy(b => topBookingIds.IndexOf(b.Id));
        }

        public async Task<bool> ExistsByPredicateAsync(Expression<Func<Booking, bool>> predicate)
        {
            return await _context.Bookings.AnyAsync(predicate);
        }
    }
}
