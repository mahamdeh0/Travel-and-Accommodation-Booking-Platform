namespace TravelAndAccommodationBookingPlatform.Core.Models
{
    public record PaginationMetadata(int TotalItemCount,  int CurrentPage, int PageSize)       
    {
        public int TotalPageCount => TotalItemCount == 0 ? 1 : (int)Math.Ceiling((double)TotalItemCount / PageSize);
    }
}