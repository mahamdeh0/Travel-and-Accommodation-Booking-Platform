using FluentValidation;
using TravelAndAccommodationBookingPlatform.WebAPI.DTOs.Authentication;

namespace TravelAndAccommodationBookingPlatform.WebAPI.Validators.Authentication
{
    public class LoginRequestValidator : AbstractValidator<LoginRequestDto>
    {
        public LoginRequestValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("A valid email is required.")
                .Matches(@"^[^@\s]+@[^@\s]+\.[^@\s]+$").WithMessage("Email format is invalid.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required.")
                .MinimumLength(8).WithMessage("Password must be at least 8 characters long.")
                .MaximumLength(100).WithMessage("Password must not exceed 100 characters.")
                .Must(password => password.Any(char.IsUpper) && password.Any(char.IsLower)).WithMessage("Password must contain both uppercase and lowercase characters.")
                .Must(password => password.Any(char.IsDigit)).WithMessage("Password must contain at least one digit.")
                .Must(password => password.Any(ch => !char.IsLetterOrDigit(ch))).WithMessage("Password must contain at least one special character.");
        }
    }
}
