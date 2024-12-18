using FluentValidation;
using TravelAndAccommodationBookingPlatform.WebAPI.DTOs.Bookings;

namespace TravelAndAccommodationBookingPlatform.WebAPI.Validators.Bookings
{
    public class BookingCreationRequestValidator : AbstractValidator<BookingCreationRequestDto>
    {
        public BookingCreationRequestValidator()
        {
            RuleFor(x => x.RoomId)
                .NotEmpty().WithMessage("At least one room ID is required.");

            RuleFor(x => x.CheckInDate)
                .NotEmpty()
                .LessThan(x => x.CheckOutDate).WithMessage("Check-in date must be before check-out date.")
                .GreaterThanOrEqualTo(DateOnly.FromDateTime(DateTime.Now));

            RuleFor(b => b.CheckOutDate)
              .NotEmpty().WithMessage("Check-out date is required.")
              .GreaterThanOrEqualTo(b => b.CheckInDate).WithMessage("Check-out date must be after or equal to the check-in date.");;

            RuleFor(x => x.PaymentMethod)
                .NotEmpty()
                .IsInEnum().WithMessage("Invalid payment method.");
        }
    }
}
