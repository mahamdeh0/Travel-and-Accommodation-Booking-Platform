namespace TravelAndAccommodationBookingPlatform.WebAPI.DTOs.Rooms
{
    public class GetRoomsRequestDto
    {
        public string? OrderDirection { get; init; }
        public string? SortColumn { get; init; }
        public string? Search { get; init; }
        public int PageNumber { get; init; } = 1;

        private int _pageSize = 10;
        public int PageSize
        {
            get => _pageSize;
            init => _pageSize = Math.Min(value, 20);
        }
    }
}

