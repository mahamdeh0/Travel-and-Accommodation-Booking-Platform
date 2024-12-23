using System.Linq.Expressions;
using TravelAndAccommodationBookingPlatform.Core.Entities;
using TravelAndAccommodationBookingPlatform.Core.Models;

namespace TravelAndAccommodationBookingPlatform.Core.Interfaces.Repositories
{
    public interface IRoomRepository
    {
        Task<Room> AddRoomAsync(Room room);
        Task UpdateRoomAsync(Room room);
        Task RemoveRoomAsync(Guid id);
        Task<bool> ExistsByPredicateAsync(Expression<Func<Room, bool>> predicate);
        Task<PaginatedResult<Room>> GetRoomsAsync(PaginatedQuery<Room> query);
        Task<Room?> GetRoomByIdAsync(Guid roomClassId, Guid id);
        Task<Room?> GetRoomWithRoomClassByIdAsync(Guid roomId);
        Task<PaginatedResult<RoomManagementDto>> GetRoomsForManagementAsync(PaginatedQuery<Room> query);
    }
}
