namespace TravelAndAccommodationBookingPlatform.Core.Interfaces.Services
{
    public interface IPdfGeneratorService
    {
        Task<byte[]> GeneratePdfFromHtmlAsync(string htmlContent);
    }
}
