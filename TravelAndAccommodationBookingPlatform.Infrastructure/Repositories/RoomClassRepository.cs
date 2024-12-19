using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TravelAndAccommodationBookingPlatform.Core.Entities;
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
            ArgumentNullException.ThrowIfNull(query);

            var currentDateTime = DateTime.UtcNow;

            var roomClassesQuery = _context.RoomClasses
                .Include(rc => rc.Discounts
                    .Where(d => currentDateTime >= d.StartDate && currentDateTime < d.EndDate))
                .Include(rc => rc.Gallery)
                .AsQueryable();

            if (!string.IsNullOrEmpty(query.SortByColumn))
                roomClassesQuery = roomClassesQuery.Sort(query.SortByColumn, query.SortDirection);

            var paginationMetadata = await roomClassesQuery.GetPaginationMetadataAsync(query.PageNumber, query.PageSize);

            var paginatedRoomClasses = await roomClassesQuery
                .GetPage(query.PageNumber, query.PageSize)
                .ToListAsync();

            return new PaginatedResult<RoomClass>(paginatedRoomClasses, paginationMetadata);
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

            var roomsWithActiveDiscounts = await _context.RoomClasses
                .Include(rc => rc.Hotel)
                    .ThenInclude(h => h.City)
                .Include(rc => rc.Gallery)
                .Include(rc => rc.Discounts)
                .Where(rc => rc.Discounts.Any(d => d.StartDate <= currentDateTime && d.EndDate > currentDateTime))
                .ToListAsync();

            var roomWithBestDiscountPerHotel = roomsWithActiveDiscounts
                .Select(rc => new
                {
                    RoomClass = rc,
                    ActiveDiscount = rc.Discounts
                        .Where(d => d.StartDate <= currentDateTime && d.EndDate > currentDateTime)
                        .OrderByDescending(d => d.Percentage)
                        .ThenBy(d => rc.NightlyRate)
                        .FirstOrDefault()
                })
                .GroupBy(x => x.RoomClass.HotelId)
                .Select(g => g
                    .OrderByDescending(x => x.ActiveDiscount.Percentage)
                    .ThenBy(x => x.RoomClass.NightlyRate)
                    .First())
                .OrderByDescending(x => x.ActiveDiscount.Percentage)
                .ThenBy(x => x.RoomClass.NightlyRate)
                .Take(count)
                .ToList();

            foreach (var item in roomWithBestDiscountPerHotel)
            {
                item.RoomClass.Discounts = new List<Discount> { item.ActiveDiscount };
            }

            return roomWithBestDiscountPerHotel.Select(x => x.RoomClass);
        }
    }
}
