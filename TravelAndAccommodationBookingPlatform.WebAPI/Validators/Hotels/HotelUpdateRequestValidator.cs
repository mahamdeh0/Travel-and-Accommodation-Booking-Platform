using FluentValidation;
using TravelAndAccommodationBookingPlatform.WebAPI.DTOs.Hotels;

namespace TravelAndAccommodationBookingPlatform.WebAPI.Validators.Hotels
{
    public class HotelUpdateRequestValidator : AbstractValidator<HotelUpdateRequestDto>
    {
        public HotelUpdateRequestValidator()
        {
            RuleFor(x => x.OwnerId)
                .NotEmpty().WithMessage("Owner ID is required.");

            RuleFor(x => x.CityId)
                .NotEmpty().WithMessage("City ID is required.");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Hotel name is required.")
                .MaximumLength(100).WithMessage("Hotel name must not exceed 100 characters.");

            RuleFor(x => x.StarRating)
                .InclusiveBetween(1, 5).WithMessage("Star rating must be between 1 and 5.");

            RuleFor(x => x.PhoneNumber)
                .Matches(@"^\+?\d{7,15}$").WithMessage("Phone number must be valid and between 7-15 digits.");

            RuleFor(x => x.Status)
                .IsInEnum().WithMessage("Invalid hotel status.");

            RuleFor(x => x.Website)
                .Matches(@"^(https?:\/\/)?([\w\-]+(\.[\w\-]+)+)(\/[\w\-]*)*(\?.*)?$")
                .WithMessage("Website must be a valid URL (e.g., https://example.com).");

            RuleFor(x => x.Geolocation)
                .NotEmpty().WithMessage("Geolocation is required.")
                .Matches(@"^-?\d{1,3}\.\d+,\s?-?\d{1,3}\.\d+$")
                .WithMessage("Geolocation must be in the format 'latitude, longitude' (e.g., 12.3456, -98.7654).");

            RuleFor(x => x.BriefDescription)
                .NotEmpty().WithMessage("Brief description is required.")
                .MaximumLength(250).WithMessage("Brief description must not exceed 250 characters.");

            RuleFor(x => x.FullDescription)
                .NotEmpty().WithMessage("Full description is required.")
                .MaximumLength(2000).WithMessage("Full description must not exceed 2000 characters.");
        }
    }
}
