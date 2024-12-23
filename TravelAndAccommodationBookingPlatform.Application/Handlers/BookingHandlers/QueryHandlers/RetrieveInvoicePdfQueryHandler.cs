using MediatR;
using TravelAndAccommodationBookingPlatform.Application.Queries.BookingQueries;
using TravelAndAccommodationBookingPlatform.Core.DomainMessages;
using TravelAndAccommodationBookingPlatform.Core.Entities;
using TravelAndAccommodationBookingPlatform.Core.Exceptions;
using TravelAndAccommodationBookingPlatform.Core.Interfaces.Repositories;
using TravelAndAccommodationBookingPlatform.Core.Interfaces.Services;

namespace TravelAndAccommodationBookingPlatform.Application.Handlers.BookingHandlers.QueryHandlers
{
    public class RetrieveInvoicePdfQueryHandler : IRequestHandler<RetrieveInvoicePdfQuery, byte[]>
    {
        private readonly IPdfGeneratorService _pdfGeneratorService;
        private readonly IBookingRepository _bookingRepository;
        private readonly IUserRepository _userRepository;
        private readonly IUserSession _userSession;

        public RetrieveInvoicePdfQueryHandler(IPdfGeneratorService pdfGeneratorService, IBookingRepository bookingRepository, IUserRepository userRepository, IUserSession userSession)
        {
            _pdfGeneratorService = pdfGeneratorService;
            _bookingRepository = bookingRepository;
            _userRepository = userRepository;
            _userSession = userSession;
        }

        public async Task<byte[]> Handle(RetrieveInvoicePdfQuery request, CancellationToken cancellationToken)
        {
            var userId = _userSession.GetUserId();
            var role = _userSession.GetUserRole();

            if (role != "Guest")
                throw new ForbiddenException(UserMessages.UserNotGuest);

            var booking = await _bookingRepository.GetBookingByIdAsync(request.BookingId, userId);
            if (booking == null)
                throw new NotFoundException(BookingMessages.BookingNotFoundForGuest);

            string htmlContent = GenerateInvoiceHtml(booking);

            return await _pdfGeneratorService.GeneratePdfFromHtmlAsync(htmlContent);
        }

        private string GenerateInvoiceHtml(Booking booking)
        {
            if (booking == null) throw new ArgumentNullException(nameof(booking));

            return $@"
        <!DOCTYPE html>
        <html lang='en'>
        <head>
            <meta charset='UTF-8'>
            <meta name='viewport' content='width=device-width, initial-scale=1.0'>
            <title>Invoice #{booking.Id}</title>
            <style>
                body {{
                    font-family: Arial, sans-serif;
                    margin: 0;
                    padding: 0;
                    line-height: 1.6;
                }}
                .container {{
                    padding: 20px;
                    max-width: 800px;
                    margin: auto;
                    border: 1px solid #ddd;
                    border-radius: 5px;
                    box-shadow: 0 2px 5px rgba(0, 0, 0, 0.1);
                }}
                .header {{
                    text-align: center;
                    margin-bottom: 20px;
                }}
                .header h1 {{
                    margin: 0;
                    color: #333;
                }}
                .details, .summary {{
                    margin-bottom: 20px;
                }}
                .details table, .summary table {{
                    width: 100%;
                    border-collapse: collapse;
                }}
                .details th, .details td, .summary th, .summary td {{
                    padding: 10px;
                    text-align: left;
                    border: 1px solid #ddd;
                }}
                .footer {{
                    text-align: center;
                    margin-top: 20px;
                    font-size: 12px;
                    color: #777;
                }}
            </style>
        </head>
        <body>
            <div class='container'>
                <div class='header'>
                    <h1>Invoice</h1>
                    <p>Booking ID: {booking.Id}</p>
                    <p>Generated on: {DateTime.Now:dddd, dd MMMM yyyy}</p>
                </div>
                <div class='details'>
                    <h2>Customer Details</h2>
                    <table>
                        <tr>
                            <th>Guest Name</th>
                            <td>{booking.Guest?.FirstName ?? "N/A"}</td>
                        </tr>
                        <tr>
                            <th>Email</th>
                            <td>{booking.Guest?.Email ?? "N/A"}</td>
                        </tr>
                        <tr>
                            <th>Phone</th>
                            <td>{booking.Guest?.PhoneNumber ?? "N/A"}</td>
                        </tr>
                    </table>
                </div>
                <div class='summary'>
                    <h2>Booking Summary</h2>
                    <table>
                        <tr>
                            <th>Hotel</th>
                            <td>{booking.Hotel?.Name ?? "N/A"}</td>
                        </tr>
                        <tr>
                            <th>Check-in Date</th>
                            <td>{booking.CheckInDate:dddd, dd MMMM yyyy}</td>
                        </tr>
                        <tr>
                            <th>Check-out Date</th>
                            <td>{booking.CheckOutDate:dddd, dd MMMM yyyy}</td>
                        </tr>
                        <tr>
                            <th>Total Nights</th>
                            <td>{(booking.CheckOutDate.ToDateTime(TimeOnly.MinValue) - booking.CheckInDate.ToDateTime(TimeOnly.MinValue)).Days}</td>
                        </tr>
                        <tr>
                            <th>Total Cost</th>
                            <td>${booking.TotalPrice:F2}</td>
                        </tr>
                    </table>
                </div>
                <div class='footer'>
                    <p>Thank you for booking with us!</p>
                </div>
            </div>
        </body>
        </html>";
        }

    }
}