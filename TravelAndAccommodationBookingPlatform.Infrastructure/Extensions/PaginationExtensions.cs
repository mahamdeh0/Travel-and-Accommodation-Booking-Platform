using Microsoft.EntityFrameworkCore;
using TravelAndAccommodationBookingPlatform.Core.Models;

namespace TravelAndAccommodationBookingPlatform.Infrastructure.Extensions
{
    public static class PaginationExtensions
    {
        public static IQueryable<T> GetPage<T>(this IQueryable<T> query, int pageNumber, int pageSize)
        {
            if (pageNumber <= 0 || pageSize <= 0)
                throw new ArgumentOutOfRangeException("PageNumber and PageSize must be greater than 0.");

            return query.Skip((pageNumber - 1) * pageSize).Take(pageSize);
        }

        public static async Task<PaginationMetadata> GetPaginationMetadataAsync<T>(this IQueryable<T> query, int pageNumber, int pageSize, CancellationToken cancellationToken = default)
        {
            var totalItemCount = await query.CountAsync(cancellationToken);
            return new PaginationMetadata(totalItemCount, pageNumber, pageSize);
        }
    }
}
