namespace TravelAndAccommodationBookingPlatform.Core.Models
{
    public record PaginatedResult<TItem>(IEnumerable<TItem> Items,PaginationMetadata PaginationMetadata);
}
