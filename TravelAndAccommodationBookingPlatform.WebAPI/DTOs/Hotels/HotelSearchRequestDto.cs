using TravelAndAccommodationBookingPlatform.Core.Enums;

namespace TravelAndAccommodationBookingPlatform.WebAPI.DTOs.Hotels
{
    public class HotelSearchRequestDto
    {
        public string? OrderDirection { get; init; }
        public string? SortColumn { get; init; }
        public string? Search { get; init; }
        public int MaxChildrenCapacity { get; set; }
        public int MaxAdultsCapacity { get; set; }
        public int NumberOfRooms { get; init; }
        public int? MinStarRating { get; init; }
        public decimal? MinPrice { get; init; }
        public decimal? MaxPrice { get; init; }
        public DateOnly CheckInDate { get; init; }
        public DateOnly CheckOutDate { get; init; }
        public IEnumerable<RoomType> RoomTypes { get; init; }
        public int PageNumber { get; init; } = 1;

        private int _pageSize = 10;
        public int PageSize
        {
            get => _pageSize;
            init => _pageSize = Math.Min(value, 20);
        }
    }
}
