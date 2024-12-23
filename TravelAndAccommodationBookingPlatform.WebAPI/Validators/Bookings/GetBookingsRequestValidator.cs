using FluentValidation;
using TravelAndAccommodationBookingPlatform.WebAPI.DTOs.Bookings;

namespace TravelAndAccommodationBookingPlatform.WebAPI.Validators.Bookings
{
    public class GetBookingsRequestValidator : AbstractValidator<GetBookingsRequestDto>
    {
        public GetBookingsRequestValidator()
        {
            RuleFor(x => x.SortColumn)
                .Must(IsValidSortColumn)
                .WithMessage($"Sort Column must be empty or one of the following: {string.Join(", ", ValidSortColumns)}.");
            RuleFor(x => x.PageNumber)
                .NotEmpty()
                .GreaterThanOrEqualTo(1);

            RuleFor(x => x.PageSize)
                .NotEmpty()
                .GreaterThanOrEqualTo(1);

            RuleFor(x => x.OrderDirection)
                .Must(x => string.IsNullOrEmpty(x) || x.Equals("asc", StringComparison.OrdinalIgnoreCase) || x.Equals("desc", StringComparison.OrdinalIgnoreCase))
                .WithMessage("OrderDirection must be either 'asc', 'desc', or empty.");

        }

        private static readonly string[] ValidSortColumns = { "id", "checkInDate", "checkOutDate", "bookingDate" };
        private static bool IsValidSortColumn(string? sortColumn) => string.IsNullOrWhiteSpace(sortColumn) || ValidSortColumns.Any(col => string.Equals(col, sortColumn, StringComparison.OrdinalIgnoreCase));
    }
}
