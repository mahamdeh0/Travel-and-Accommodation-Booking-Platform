using TravelAndAccommodationBookingPlatform.Core.Entities;

namespace TravelAndAccommodationBookingPlatform.Application.Handlers.BookingHandlers.Shared
{
    public static class InvoiceHtmlGenerator
    {
        public static string GenerateInvoiceHtml(Booking booking)
        {
            if (booking == null)
                throw new ArgumentNullException(nameof(booking));

            var nights = (booking.CheckOutDate.ToDateTime(TimeOnly.MinValue) - booking.CheckInDate.ToDateTime(TimeOnly.MinValue)).Days;
            if (nights < 1) nights = 1;

            string checkInStr = booking.CheckInDate.ToString("yyyy-MM-dd");
            string checkOutStr = booking.CheckOutDate.ToString("yyyy-MM-dd");
            string bookingDateStr = booking.BookingDate.ToString("yyyy-MM-dd");

            var invoiceRows = booking.Invoice?.Select(inv => $@"
            <tr>
                <td>{inv.RoomNumber}</td>
                <td>{inv.RoomClassName}</td>
                <td>{inv.PriceAtReservation:C}</td>
                <td>{(inv.DiscountAppliedAtBooking ?? 0)}%</td>
                <td>{(inv.AmountAfterDiscount ?? inv.PriceAtReservation):C}</td>
                <td>{(inv.TaxAmount ?? 0):C}</td>
                <td>{(inv.AdditionalCharges ?? 0):C}</td>
                <td>{inv.TotalAmount:C}</td>
            </tr>
        ") ?? Enumerable.Empty<string>();

            return $@"
            <!DOCTYPE html>
            <html lang='en'>
            <head>
                <meta charset='UTF-8'>
                <title>Invoice #{booking.Id}</title>
                <style>
                    body {{ 
                        font-family: Arial, sans-serif; 
                        margin: 0; 
                        padding:0; 
                        background: #f9f9f9; 
                        color: #333;
                    }}
                    .container {{
                        background: #fff;
                        margin: 20px auto;
                        padding: 20px;
                        max-width: 800px;
                        box-shadow: 0 0 10px rgba(0,0,0,0.1);
                    }}
                    h1, h2, h3 {{
                        margin-bottom: 10px;
                        color: #333;
                    }}
                    .header {{
                        text-align: center;
                        margin-bottom: 40px;
                    }}
                    .header h1 {{
                        margin: 0;
                        font-size: 28px;
                        text-transform: uppercase;
                        letter-spacing: 2px;
                    }}
                    .section {{
                        margin-bottom: 30px;
                    }}
                    .section h2 {{
                        border-bottom: 2px solid #ddd;
                        padding-bottom: 5px;
                        font-size: 20px;
                        margin-bottom: 15px;
                    }}
                    p {{
                        margin: 5px 0;
                    }}
                    table {{
                        width: 100%; 
                        border-collapse: collapse; 
                        margin-bottom: 20px; 
                        font-size: 14px;
                    }}
                    table, th, td {{ 
                        border: 1px solid #ddd; 
                    }}
                    th, td {{
                        padding: 10px; 
                        text-align: left; 
                    }}
                    th {{
                        background: #f2f2f2;
                        font-weight: bold;
                    }}
                    .total {{
                        font-weight: bold;
                        font-size: 16px;
                    }}
                    .footer {{
                        text-align: center;
                        font-size: 12px;
                        color: #888;
                        margin-top: 30px;
                    }}
                </style>
            </head>
            <body>
                <div class='container'>
                    <div class='header'>
                        <h1>Invoice</h1>
                        <p>#{booking.Id}</p>
                    </div>

                    <div class='section'>
                        <h2>Booking Details</h2>
                        <p><strong>Booking ID:</strong> {booking.Id}</p>
                        <p><strong>Hotel:</strong> {booking.Hotel?.Name ?? "N/A"}</p>
                        <p><strong>City:</strong> {booking.Hotel?.City?.Name ?? "N/A"}</p>
                        <p><strong>Check-In:</strong> {checkInStr}</p>
                        <p><strong>Check-Out:</strong> {checkOutStr}</p>
                        <p><strong>Nights:</strong> {nights}</p>
                        <p><strong>Booking Date:</strong> {bookingDateStr}</p>
                        <p class='total'><strong>Total Price:</strong> {booking.TotalPrice:C}</p>
                    </div>

                    <div class='section'>
                        <h2>Guest Information</h2>
                        <p><strong>Name:</strong> {booking.Guest?.FirstName ?? ""} {booking.Guest?.LastName ?? ""}</p>
                        <p><strong>Email:</strong> {booking.Guest?.Email ?? "N/A"}</p>
                        <p><strong>Phone:</strong> {booking.Guest?.PhoneNumber ?? "N/A"}</p>
                    </div>

                    <div class='section'>
                        <h2>Invoice Details</h2>
                        <table>
                            <tr>
                                <th>Room Number</th>
                                <th>Class</th>
                                <th>Price/Night</th>
                                <th>Discount</th>
                                <th>After Discount</th>
                                <th>Tax</th>
                                <th>Additional Charges</th>
                                <th>Line Total</th>
                            </tr>
                            {string.Join("", invoiceRows)}
                        </table>
                    </div>

                    <p>Thank you for booking with us!</p>

                    <div class='footer'>
                        <p>&copy; {DateTime.Now.Year} Your Company Name. All Rights Reserved.</p>
                    </div>
                </div>
            </body>
            </html>";
        }
    }
}
