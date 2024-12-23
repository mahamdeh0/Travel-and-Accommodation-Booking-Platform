using Microsoft.AspNetCore.Http;
using TravelAndAccommodationBookingPlatform.Core.Entities;
using TravelAndAccommodationBookingPlatform.Core.Enums;

namespace TravelAndAccommodationBookingPlatform.Core.Interfaces.Repositories
{
    public interface IImageRepository
    {
        Task<Image> UploadImageAsync(IFormFile image, Guid entityId, ImageType type);

        Task RemoveImageAsync(Guid entityId, ImageType type);
    }
}
