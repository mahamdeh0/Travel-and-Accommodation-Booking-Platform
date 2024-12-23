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
            var queryable = _context.Bookings.Include(b => b.Hotel).AsQueryable();

            if (query.FilterExpression != null)
                queryable = queryable.Where(query.FilterExpression);

            queryable = queryable.Sort(query.SortByColumn, query.SortDirection);

            var paginationMetadata = await queryable.GetPaginationMetadataAsync(query.PageNumber, query.PageSize);
            var pagedItems = await queryable.GetPage(query.PageNumber, query.PageSize).AsNoTracking().ToListAsync();

            return new PaginatedResult<Booking>(pagedItems, paginationMetadata);
        }

        public async Task<IEnumerable<Booking>> GetRecentBookingsByGuestIdAsync(Guid guestId, int count)
        {
            var groupedBookings = await _context.Bookings
                .Where(b => b.GuestId == guestId)
                .GroupBy(b => b.HotelId)
                .ToListAsync();

            var latestBookings = groupedBookings
                .Select(g => g.OrderByDescending(b => b.CheckInDate).FirstOrDefault())
                .Where(b => b != null)
                .OrderByDescending(b => b.CheckInDate)
                .Take(count)
                .ToList();

            if (!latestBookings.Any())
                return Enumerable.Empty<Booking>();

            var bookingIds = latestBookings.Select(b => b.Id).ToList();

            var recentBookingsWithImages = await _context.Bookings
                .Where(b => bookingIds.Contains(b.Id))
                .Include(b => b.Hotel)
                    .ThenInclude(h => h.City)
                .ToListAsync();

            var bookingsWithImages = recentBookingsWithImages
                .GroupJoin(
                    _context.Images.Where(img => img.Type == ImageType.Thumbnail),
                    b => b.Hotel.Id,
                    img => img.EntityId,
                    (booking, images) => new
                    {
                        Booking = booking,
                        Thumbnail = images.FirstOrDefault()
                    }
                )
                .ToList();

            foreach (var item in bookingsWithImages)
            {
                if (item.Thumbnail != null)
                {
                    item.Booking.Hotel.Thumbnail = item.Thumbnail;
                }
            }

            return bookingsWithImages
                .Select(x => x.Booking)
                .OrderBy(b => bookingIds.IndexOf(b.Id));
        }

        public async Task<bool> ExistsByPredicateAsync(Expression<Func<Booking, bool>> predicate)
        {
            return await _context.Bookings.AnyAsync(predicate);
        }
    }
}
