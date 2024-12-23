namespace TravelAndAccommodationBookingPlatform.Core.Models
{
    public record EmailRequest(IEnumerable<string> ToEmails, string SubjectLine, string MessageBody, IEnumerable<FileAttachment> FileAttachment);
}
