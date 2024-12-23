using FluentValidation;
using TravelAndAccommodationBookingPlatform.WebAPI.DTOs.Cities;

namespace TravelAndAccommodationBookingPlatform.WebAPI.Validators.Cities
{
    public class GetTrendingCitiesRequestValidator : AbstractValidator<GetTrendingCitiesRequestDto>
    {
        public GetTrendingCitiesRequestValidator()
        {
            RuleFor(x => x.Count).InclusiveBetween(1, 100).WithMessage("Count must be between 1 and 100."); ;
        }
    }
}
