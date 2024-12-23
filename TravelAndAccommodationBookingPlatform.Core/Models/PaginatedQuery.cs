using System.Linq.Expressions;
using TravelAndAccommodationBookingPlatform.Core.Enums;

namespace TravelAndAccommodationBookingPlatform.Core.Models
{
    public record PaginatedQuery<TEntity>(Expression<Func<TEntity, bool>> FilterExpression,string? SortByColumn,int PageNumber,int PageSize,OrderDirection SortDirection = OrderDirection.Ascending);
}
