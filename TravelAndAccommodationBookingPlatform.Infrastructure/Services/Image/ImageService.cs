using Firebase.Storage;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Storage.V1;
using Microsoft.AspNetCore.Http;
using TravelAndAccommodationBookingPlatform.Core.Interfaces.Services;

namespace TravelAndAccommodationBookingPlatform.Infrastructure.Services.Image
{
    public class ImageService : IImageService
    {
        private static readonly string[] AllowedImageFormats = { ".jpg", ".jpeg", ".png" };
        private readonly FirebaseConfig _firebaseConfig;

        public ImageService(FirebaseConfig firebaseConfig)
        {
            _firebaseConfig = firebaseConfig;
        }

        public async Task<Core.Entities.Image> StoreAsync(IFormFile image)
        {
            if (image == null || image.Length <= 0)
                throw new ArgumentNullException(nameof(image), "The provided image is null or empty.");

            var imageFormat = Path.GetExtension(image.FileName).ToLower();

            if (!AllowedImageFormats.Contains(imageFormat))
                throw new ArgumentOutOfRangeException(nameof(image), $"The image format '{imageFormat}' is not supported.");

            var credential = GoogleCredential.FromFile(_firebaseConfig.GoogleCredential);

            var storage = await StorageClient.CreateAsync(credential);

            var imageEntity = new Core.Entities.Image
            {
                Path = string.Empty
            };

            var objectName = $"{Guid.NewGuid()}{imageFormat}";

            using (var stream = image.OpenReadStream())
            {
                await storage.UploadObjectAsync(
                    _firebaseConfig.Bucket,
                    objectName,
                    image.ContentType,
                    stream
                );
            }

            imageEntity.Path = await GetImagePublicUrl(objectName);

            return imageEntity;
        }

        public async Task RemoveAsync(Core.Entities.Image image)
        {
            if (image == null || string.IsNullOrEmpty(image.Path))
                throw new ArgumentNullException(nameof(image), "The provided image is null or invalid.");

            var credential = GoogleCredential.FromFile(_firebaseConfig.GoogleCredential);

            var storage = await StorageClient.CreateAsync(credential);

            var objectName = Path.GetFileName(new Uri(image.Path).AbsolutePath);

            await storage.DeleteObjectAsync(
                _firebaseConfig.Bucket,
                objectName
            );
        }

        private async Task<string> GetImagePublicUrl(string objectName)
        {
            var storage = new FirebaseStorage(_firebaseConfig.Bucket);

            var starsRef = storage.Child(objectName);

            return await starsRef.GetDownloadUrlAsync();
        }
    }
}
