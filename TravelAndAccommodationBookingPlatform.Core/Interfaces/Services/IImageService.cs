using Microsoft.AspNetCore.Http;
using TravelAndAccommodationBookingPlatform.Core.Entities;

namespace TravelAndAccommodationBookingPlatform.Core.Interfaces.Services
{
    public interface IImageService
    {
        Task<Image> StoreAsync(IFormFile image);

        Task RemoveAsync(Image image);
    }
}
