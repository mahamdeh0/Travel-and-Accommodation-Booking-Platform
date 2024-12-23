using FluentValidation;

namespace TravelAndAccommodationBookingPlatform.Infrastructure.Auth
{
    public class JwtSettingsValidator : AbstractValidator<JwtSettings>
    {
        public JwtSettingsValidator()
        {
            RuleFor(x => x.Key).NotEmpty();

            RuleFor(x => x.Issuer).NotEmpty();

            RuleFor(x => x.Audience).NotEmpty();

            RuleFor(x => x.LifetimeMinutes).NotEmpty().GreaterThan(0);
        }
    }
}
