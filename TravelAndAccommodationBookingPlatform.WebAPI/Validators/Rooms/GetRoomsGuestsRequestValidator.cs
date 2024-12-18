using FluentValidation;
using TravelAndAccommodationBookingPlatform.WebAPI.DTOs.Rooms;

namespace TravelAndAccommodationBookingPlatform.WebAPI.Validators.Rooms
{
    public class GetRoomsGuestsRequestValidator : AbstractValidator<GetRoomsGuestsRequestDto>
    {
        public GetRoomsGuestsRequestValidator()
        {
            RuleFor(x => x.SortColumn)
                .Must(IsValidSortColumn)
                .WithMessage($"Sort Column must be empty or one of: {string.Join(", ", ValidSortColumns)}.");

            RuleFor(x => x.PageNumber)
                .NotEmpty()
                .GreaterThanOrEqualTo(1);

            RuleFor(x => x.PageSize)
                .NotEmpty()
                .GreaterThanOrEqualTo(1);

            RuleFor(x => x.OrderDirection)
                .Must(x => string.IsNullOrEmpty(x) || x.Equals("asc", StringComparison.OrdinalIgnoreCase) || x.Equals("desc", StringComparison.OrdinalIgnoreCase))
                .WithMessage("OrderDirection must be either 'asc', 'desc', or empty.");

            RuleFor(x => x.CheckInDate)
                .NotEmpty()
                .WithMessage("CheckInDate is required.");

            RuleFor(x => x.CheckOutDate)
                .NotEmpty()
                .WithMessage("CheckOutDate is required.")
                .GreaterThan(x => x.CheckInDate).WithMessage("CheckOutDate must be after CheckInDate.");
        }

        private static readonly string[] ValidSortColumns = { "id", "number" };
        private static bool IsValidSortColumn(string? sortColumn) => string.IsNullOrWhiteSpace(sortColumn) || ValidSortColumns.Any(col => string.Equals(col, sortColumn, StringComparison.OrdinalIgnoreCase));
    }
}
