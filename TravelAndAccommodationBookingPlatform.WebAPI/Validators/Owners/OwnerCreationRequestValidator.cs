using FluentValidation;
using TravelAndAccommodationBookingPlatform.WebAPI.DTOs.Owners;

namespace TravelAndAccommodationBookingPlatform.WebAPI.Validators.Owners
{
    public class OwnerCreationRequestValidator : AbstractValidator<OwnerCreationRequestDto>
    {
        public OwnerCreationRequestValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("First name is required.")
                .MaximumLength(50).WithMessage("First name must not exceed 50 characters.");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Last name is required.")
                .MaximumLength(50).WithMessage("Last name must not exceed 50 characters.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("A valid email is required.");

            RuleFor(x => x.PhoneNumber)
                .Matches(@"^\+?\d{7,15}$").WithMessage("Phone number must be valid and between 7-15 digits.");
        }
    }
}
