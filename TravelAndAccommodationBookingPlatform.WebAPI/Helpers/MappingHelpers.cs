using TravelAndAccommodationBookingPlatform.Core.Enums;

namespace TravelAndAccommodationBookingPlatform.WebAPI.Helpers
{
    public static class MappingHelpers
    {
        public static OrderDirection? MapOrderDirection(string orderDirection)
        {
            return orderDirection?.ToLower() switch
            {
                "asc" => OrderDirection.Ascending,
                "desc" => OrderDirection.Descending,
                _ => null
            };
        }
    }
}
