using System.Linq.Expressions;
using TravelAndAccommodationBookingPlatform.Core.Enums;

namespace TravelAndAccommodationBookingPlatform.Infrastructure.Extensions
{
    public static class SortingExtensions
    {
        public static IQueryable<T> Sort<T>(this IQueryable<T> queryable, string? sortColumn, OrderDirection sortDirection)
        {
            if (string.IsNullOrEmpty(sortColumn)) return queryable;

            var parameter = Expression.Parameter(typeof(T), "x");
            var property = Expression.PropertyOrField(parameter, sortColumn);
            var lambda = Expression.Lambda<Func<T, object>>(Expression.Convert(property, typeof(object)), parameter);

            return sortDirection == OrderDirection.Ascending ? queryable.OrderBy(lambda) : queryable.OrderByDescending(lambda);
        }
    }
}
