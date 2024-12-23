using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TravelAndAccommodationBookingPlatform.Core.DomainMessages;
using TravelAndAccommodationBookingPlatform.Core.Entities;
using TravelAndAccommodationBookingPlatform.Core.Exceptions;
using TravelAndAccommodationBookingPlatform.Core.Interfaces.Repositories;
using TravelAndAccommodationBookingPlatform.Core.Models;
using TravelAndAccommodationBookingPlatform.Infrastructure.Data;
using TravelAndAccommodationBookingPlatform.Infrastructure.Extensions;

namespace TravelAndAccommodationBookingPlatform.Infrastructure.Repositories
{
    public class RoomRepository : IRoomRepository
    {
        private readonly AppDbContext _context;

        public RoomRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Room> AddRoomAsync(Room room)
        {
            ArgumentNullException.ThrowIfNull(room);

            var newRoom = await _context.Rooms.AddAsync(room);

            return newRoom.Entity;
        }

        public async Task UpdateRoomAsync(Room room)
        {
            ArgumentNullException.ThrowIfNull(room);

            if (!await _context.Rooms.AnyAsync(r => r.Id == room.Id))
                throw new NotFoundException(RoomMessages.RoomNotFound);

            _context.Rooms.Update(room);
        }

        public async Task RemoveRoomAsync(Guid id)
        {
            var room = await _context.Rooms.FindAsync(id);
            if (room != null)
                _context.Rooms.Remove(room);
        }

        public async Task<bool> ExistsByPredicateAsync(Expression<Func<Room, bool>> predicate)
        {
            return await _context.Rooms.AnyAsync(predicate);
        }

        public async Task<PaginatedResult<Room>> GetRoomsAsync(PaginatedQuery<Room> query)
        {
            ArgumentNullException.ThrowIfNull(query);

            var queryable = _context.Rooms.AsQueryable();

            if (query.FilterExpression != null)
                queryable = queryable.Where(query.FilterExpression);

            if (!string.IsNullOrEmpty(query.SortByColumn))
                queryable = queryable.Sort(query.SortByColumn, query.SortDirection);

            var paginationMetadata = await queryable.GetPaginationMetadataAsync(query.PageNumber, query.PageSize);

            var pagedItems = await queryable
                .GetPage(query.PageNumber, query.PageSize)
                .AsNoTracking()
                .ToListAsync();

            return new PaginatedResult<Room>(pagedItems, paginationMetadata);
        }


        public async Task<Room?> GetRoomByIdAsync(Guid roomClassId, Guid id)
        {
            return await _context.Rooms.FirstOrDefaultAsync(r => r.RoomClassId == roomClassId && r.Id == id);
        }

        public async Task<Room?> GetRoomWithRoomClassByIdAsync(Guid roomId)
        {
            var currentDateTime = DateTime.UtcNow;

            return await _context.Rooms
                .Include(r => r.RoomClass)
                .ThenInclude(rc => rc.Discounts.Where(d => d.StartDate <= currentDateTime && d.EndDate > currentDateTime))
                .FirstOrDefaultAsync(r => r.Id == roomId);
        }

        public async Task<PaginatedResult<RoomManagementDto>> GetRoomsForManagementAsync(PaginatedQuery<Room> query)
        {
            ArgumentNullException.ThrowIfNull(query);
            var currentDate = DateOnly.FromDateTime(DateTime.Now);

            var queryable = _context.Rooms.AsQueryable();

            if (query.FilterExpression != null)
                queryable = queryable.Where(query.FilterExpression);

            if (!string.IsNullOrEmpty(query.SortByColumn))
                queryable = queryable.Sort(query.SortByColumn, query.SortDirection);

            var paginationMetadata = await queryable.GetPaginationMetadataAsync(query.PageNumber, query.PageSize);

            var pagedItems = await queryable
                .GetPage(query.PageNumber, query.PageSize)
                .Select(room => new RoomManagementDto
                {
                    Id = room.Id,
                    RoomClassId = room.RoomClassId,
                    IsAvailable = !room.Bookings.Any(b => b.CheckInDate >= currentDate && b.CheckOutDate <= currentDate),
                    Number = room.Number,
                    CreatedAt = room.CreatedAt,
                    UpdatedAt = room.UpdatedAt
                })
                .AsNoTracking()
                .ToListAsync();

            return new PaginatedResult<RoomManagementDto>(pagedItems, paginationMetadata);
        }

    }
}
