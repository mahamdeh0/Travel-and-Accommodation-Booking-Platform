using FluentValidation;
using TravelAndAccommodationBookingPlatform.WebAPI.DTOs.Reviews;

namespace TravelAndAccommodationBookingPlatform.WebAPI.Validators.Reviews
{
    public class ReviewUpdateRequestValidator : AbstractValidator<ReviewUpdateRequestDto>
    {
        public ReviewUpdateRequestValidator()
        {
            RuleFor(x => x.Content)
                .NotEmpty().WithMessage("Review content is required.")
                .MaximumLength(500).WithMessage("Review content must not exceed 500 characters.");

            RuleFor(x => x.Rating)
                .InclusiveBetween(1, 5).WithMessage("Rating must be between 1 and 5.");
        }
    }
}
