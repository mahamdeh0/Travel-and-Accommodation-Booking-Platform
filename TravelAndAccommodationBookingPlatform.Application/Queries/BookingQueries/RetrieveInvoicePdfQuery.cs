using MediatR;

namespace TravelAndAccommodationBookingPlatform.Application.Queries.BookingQueries
{
    public class RetrieveInvoicePdfQuery : IRequest<byte[]>
    {
        public Guid BookingId { get; init; }
    }
}
