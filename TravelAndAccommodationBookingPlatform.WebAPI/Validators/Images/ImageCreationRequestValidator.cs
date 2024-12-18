using FluentValidation;
using TravelAndAccommodationBookingPlatform.WebAPI.DTOs.Images;

namespace TravelAndAccommodationBookingPlatform.WebAPI.Validators.Images
{
    public class ImageCreationRequestValidator : AbstractValidator<ImageCreationRequestDto>
    {
        private readonly string[] allowedImageTypes = { "image/jpg", "image/jpeg", "image/png" };

        public ImageCreationRequestValidator()
        {
            RuleFor(x => x.Image)
                .NotNull().WithMessage("Image file is required.")
                .Must(image => image.Length > 0).WithMessage("Image file must not be empty.")
                .Must(image => IsAllowedImageType(image.ContentType))
                .WithMessage("Image file must be of type JPG, JPEG, or PNG.");
        }

        private bool IsAllowedImageType(string contentType)
        {
            return allowedImageTypes.Contains(contentType);
        }
    }
}
