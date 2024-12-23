using FluentValidation;
using TravelAndAccommodationBookingPlatform.WebAPI.DTOs.Cities;

namespace TravelAndAccommodationBookingPlatform.WebAPI.Validators.Cities
{
    public class GetCitiesRequestValidator : AbstractValidator<GetCitiesRequestDto>
    {
        public GetCitiesRequestValidator()
        {
            RuleFor(x => x.Search).MaximumLength(100);

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
                .Must(x => string.IsNullOrEmpty(x) || x.Equals("asc", System.StringComparison.OrdinalIgnoreCase) || x.Equals("desc", System.StringComparison.OrdinalIgnoreCase))
                .WithMessage("OrderDirection must be either 'asc', 'desc', or empty.");
        }

        private static readonly string[] ValidSortColumns = { "id", "Name", "Country", "PostOffice" };
        private static bool IsValidSortColumn(string? sortColumn) => string.IsNullOrWhiteSpace(sortColumn) || ValidSortColumns.Any(col => string.Equals(col, sortColumn, StringComparison.OrdinalIgnoreCase));
    }
}
