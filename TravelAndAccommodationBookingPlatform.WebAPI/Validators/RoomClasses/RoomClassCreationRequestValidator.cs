using FluentValidation;
using TravelAndAccommodationBookingPlatform.WebAPI.DTOs.RoomClasses;

namespace TravelAndAccommodationBookingPlatform.WebAPI.Validators.RoomClasses
{
    public class RoomClassCreationRequestValidator : AbstractValidator<RoomClassCreationRequestDto>
    {
        public RoomClassCreationRequestValidator()
        {
            RuleFor(x => x.HotelId)
                .NotEmpty().WithMessage("Hotel ID is required.");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Room class name is required.")
                .MaximumLength(100).WithMessage("Room class name must not exceed 100 characters.");

            RuleFor(x => x.MaxChildrenCapacity)
                .NotNull().WithMessage("Max children capacity is required.")
                .GreaterThanOrEqualTo(0).WithMessage("Max children capacity must be at least 0.");

            RuleFor(x => x.MaxAdultsCapacity)
                .NotNull().WithMessage("Max adults capacity is required.")
                .GreaterThan(0).WithMessage("Max adults capacity must be greater than 0.");

            RuleFor(x => x.NightlyRate)
                .NotNull().WithMessage("Nightly rate is required.")
                .GreaterThan(0).WithMessage("Nightly rate must be greater than 0.");
        }
    }
}
