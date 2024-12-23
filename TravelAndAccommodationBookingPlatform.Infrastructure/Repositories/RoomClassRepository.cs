using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TravelAndAccommodationBookingPlatform.Core.Entities;
using TravelAndAccommodationBookingPlatform.Core.Enums;
using TravelAndAccommodationBookingPlatform.Core.Interfaces.Repositories;
using TravelAndAccommodationBookingPlatform.Core.Models;
using TravelAndAccommodationBookingPlatform.Infrastructure.Data;
using TravelAndAccommodationBookingPlatform.Infrastructure.Extensions;

namespace TravelAndAccommodationBookingPlatform.Infrastructure.Repositories
{
    public class RoomClassRepository : IRoomClassRepository
    {
        private readonly AppDbContext _context;

        public RoomClassRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<PaginatedResult<RoomClass>> GetRoomClassesAsync(PaginatedQuery<RoomClass> query)
        {
            if (query == null) throw new ArgumentNullException(nameof(query));

            var currentDateTime = DateTime.UtcNow;

            var roomClassesQuery = _context.RoomClasses
                .Include(rc => rc.Discounts.Where(d => currentDateTime >= d.StartDate && currentDateTime < d.EndDate))
                .AsNoTracking()
                .Select(rc => new
                {
                    RoomClass = rc,
                    Gallery = _context.Images.Where(img => img.EntityId == rc.Id).ToList()
                });

            if (!string.IsNullOrEmpty(query.SortByColumn))
                roomClassesQuery = roomClassesQuery.Sort(query.SortByColumn, query.SortDirection);

            var paginationMetadata = await roomClassesQuery.GetPaginationMetadataAsync(query.PageNumber, query.PageSize);

            var paginatedRoomClasses = await roomClassesQuery
                .GetPage(query.PageNumber, query.PageSize)
                .ToListAsync();

            foreach (var item in paginatedRoomClasses)
                item.RoomClass.Gallery = item.Gallery;

            return new PaginatedResult<RoomClass>(paginatedRoomClasses.Select(x => x.RoomClass), paginationMetadata);
        }

        public async Task<RoomClass?> GetRoomClassByIdAsync(Guid id)
        {
            return await _context.RoomClasses.FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<RoomClass> AddRoomClassAsync(RoomClass roomClass)
        {
            ArgumentNullException.ThrowIfNull(roomClass);

            var newRoomClass = await _context.RoomClasses.AddAsync(roomClass);

            return newRoomClass.Entity;
        }

        public async Task UpdateRoomClassAsync(RoomClass roomClass)
        {
            ArgumentNullException.ThrowIfNull(roomClass);

            var existingRoomClass = await _context.RoomClasses.FindAsync(roomClass.Id);
            if (existingRoomClass == null) return;

            _context.RoomClasses.Update(roomClass);
        }

        public async Task RemoveRoomClassAsync(Guid id)
        {
            var roomClass = await _context.RoomClasses.FindAsync(id);
            if (roomClass == null) return;

            _context.RoomClasses.Remove(roomClass);
        }

        public async Task<bool> ExistsAsync(Expression<Func<RoomClass, bool>> predicate)
        {
            return await _context.RoomClasses.AnyAsync(predicate);
        }

        public async Task<IEnumerable<RoomClass>> GetFeaturedRoomsAsync(int count)
        {
            var currentDateTime = DateTime.UtcNow;

            var featuredDeals = await _context.RoomClasses
                .AsNoTracking()
                .Where(rc => rc.Discounts.Any(d =>
                    d.StartDate <= currentDateTime &&
                    d.EndDate > currentDateTime))
                .Select(rc => new
                {
                    RoomClass = rc,
                    ActiveDiscount = rc.Discounts
                        .Where(d => d.StartDate <= currentDateTime && d.EndDate > currentDateTime)
                        .OrderByDescending(d => d.Percentage)
                        .FirstOrDefault(),
                    Hotel = rc.Hotel,
                    Thumbnail = _context.Images
                        .Where(img => img.EntityId == rc.HotelId && img.Type == ImageType.Thumbnail)
                        .FirstOrDefault()
                })
                .GroupBy(rcd => rcd.Hotel.Id)
                .Select(g => g
                    .OrderByDescending(rcd => rcd.ActiveDiscount.Percentage)
                    .ThenBy(rcd => rcd.RoomClass.NightlyRate)
                    .FirstOrDefault())
                .Take(count)
                .ToListAsync();

            var result = featuredDeals.Select(d =>
            {
                d.RoomClass.Discounts = new List<Discount> { d.ActiveDiscount };
                d.Hotel.Thumbnail = d.Thumbnail;
                d.RoomClass.Hotel = d.Hotel;
                return d.RoomClass;
            });

            return result;
        }

    }
}
