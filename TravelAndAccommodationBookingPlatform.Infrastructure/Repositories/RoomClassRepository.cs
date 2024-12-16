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

        //Todo
        public Task<IEnumerable<RoomClass>> GetFeaturedRoomsAsync(int count)
        {
            throw new NotImplementedException();
        }
    }
}
