using System.Linq.Expressions;
using TravelAndAccommodationBookingPlatform.Core.Entities;
using TravelAndAccommodationBookingPlatform.Core.Models;

namespace TravelAndAccommodationBookingPlatform.Core.Interfaces.Repositories
{
    public interface IRoomClassRepository
    {
        Task<PaginatedResult<RoomClass>> GetRoomClassesAsync(PaginatedQuery<RoomClass> query);
        Task<RoomClass?> GetRoomClassByIdAsync(Guid id);
        Task<RoomClass> AddRoomClassAsync(RoomClass roomClass);
        Task UpdateRoomClassAsync(RoomClass roomClass);
        Task RemoveRoomClassAsync(Guid id);
        Task<IEnumerable<RoomClass>> GetFeaturedRoomsAsync(int count);
        Task<bool> ExistsAsync(Expression<Func<RoomClass, bool>> predicate);
    }
}
