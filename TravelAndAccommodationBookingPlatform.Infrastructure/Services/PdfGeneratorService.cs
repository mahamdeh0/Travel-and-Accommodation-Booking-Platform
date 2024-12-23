using NReco.PdfGenerator;
using TravelAndAccommodationBookingPlatform.Core.Interfaces.Services;

namespace TravelAndAccommodationBookingPlatform.Infrastructure.Services
{
    public class PdfGeneratorService : IPdfGeneratorService
    {
        public async Task<byte[]> GeneratePdfFromHtmlAsync(string htmlContent)
        {
            var converter = new HtmlToPdfConverter();
            return await Task.FromResult(converter.GeneratePdf(htmlContent));
        }
    }
}
