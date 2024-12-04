namespace TravelAndAccommodationBookingPlatform.Core.Models
{
    public record FileAttachment(
        string FileName,         
        string MediaType,        
        byte[] FileContent      
    );
}
