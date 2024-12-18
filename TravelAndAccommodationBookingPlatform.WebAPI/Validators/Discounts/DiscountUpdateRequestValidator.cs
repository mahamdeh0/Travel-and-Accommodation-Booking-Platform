using FluentValidation;
using TravelAndAccommodationBookingPlatform.WebAPI.DTOs.Discounts;

namespace TravelAndAccommodationBookingPlatform.WebAPI.Validators.Discounts
{
    public class DiscountUpdateRequestValidator : AbstractValidator<DiscountUpdateRequestDto>
    {
        public DiscountUpdateRequestValidator()
        {
            RuleFor(x => x.Percentage)
                .NotEmpty().WithMessage("Discount percentage is required.")
                .InclusiveBetween(0, 100).WithMessage("Discount percentage must be between 0 and 100.");

            RuleFor(x => x.StartDate)
                .NotEmpty().WithMessage("Start date is required.")
                .LessThan(x => x.EndDate).WithMessage("Start date must be before the end date.");

            RuleFor(x => x.EndDate)
                .NotEmpty().WithMessage("End date is required.")
                .GreaterThan(x => x.StartDate).WithMessage("End date must be after the start date.");
        }
    }
}
