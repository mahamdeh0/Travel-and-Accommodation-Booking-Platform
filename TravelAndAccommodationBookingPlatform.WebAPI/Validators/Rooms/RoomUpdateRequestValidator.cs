﻿using FluentValidation;
using TravelAndAccommodationBookingPlatform.WebAPI.DTOs.Rooms;

namespace TravelAndAccommodationBookingPlatform.WebAPI.Validators.Rooms
{
    public class RoomUpdateRequestValidator : AbstractValidator<RoomUpdateRequestDto>
    {
        public RoomUpdateRequestValidator()
        {
            RuleFor(x => x.Number)
               .NotEmpty().WithMessage("Room number is required.")
               .MaximumLength(10).WithMessage("Room number must not exceed 10 characters.");
        }
    }
}