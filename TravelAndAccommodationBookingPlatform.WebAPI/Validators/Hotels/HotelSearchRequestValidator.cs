using FluentValidation;
using TravelAndAccommodationBookingPlatform.WebAPI.DTOs.Hotels;

namespace TravelAndAccommodationBookingPlatform.WebAPI.Validators.Hotels
{
    public class HotelSearchRequestValidator : AbstractValidator<HotelSearchRequestDto>
    {
        public HotelSearchRequestValidator()
        {
            RuleFor(x => x.Search)
                .MaximumLength(100);

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

            RuleFor(x => x.MaxAdultsCapacity)
                .GreaterThanOrEqualTo(0).WithMessage("MaxAdultsCapacity must be greater than or equal to 0.");

            RuleFor(x => x.MaxChildrenCapacity)
                .GreaterThanOrEqualTo(0).WithMessage("MaxChildrenCapacity must be greater than or equal to 0.");

            RuleFor(x => x.MinPrice)
                .GreaterThanOrEqualTo(0).When(x => x.MinPrice.HasValue)
                .WithMessage("MinPrice must be greater than or equal to 0.");

            RuleFor(x => x.MaxPrice)
                .GreaterThanOrEqualTo(x => x.MinPrice.GetValueOrDefault(0))
                .When(x => x.MaxPrice.HasValue && x.MinPrice.HasValue)
                .WithMessage("MaxPrice must be greater than or equal to MinPrice.");

            RuleFor(x => x.MinStarRating)
                .InclusiveBetween(1, 5).When(x => x.MinStarRating.HasValue)
                .WithMessage("MinStarRating must be between 1 and 5.");

            RuleFor(x => x.NumberOfRooms)
                .NotEmpty()
                .GreaterThanOrEqualTo(1)
                .WithMessage("NumberOfRooms must be greater or equal to 1.");

            RuleFor(x => x.CheckInDate)
                .NotEmpty()
                .WithMessage("CheckInDate is required.");

            RuleFor(x => x.CheckOutDate)
                .NotEmpty()
                .WithMessage("CheckOutDate is required.")
                .GreaterThan(x => x.CheckInDate).WithMessage("CheckOutDate must be after CheckInDate.");
        }

        private static readonly string[] ValidSortColumns = { "id", "name", "reviewsRating", "starRating", "price" };
        private static bool IsValidSortColumn(string? sortColumn) => string.IsNullOrWhiteSpace(sortColumn) || ValidSortColumns.Any(col => string.Equals(col, sortColumn, StringComparison.OrdinalIgnoreCase));
    }
}
