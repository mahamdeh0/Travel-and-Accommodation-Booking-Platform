using MediatR;
using TravelAndAccommodationBookingPlatform.Application.Handlers.BookingHandlers.Shared;
using TravelAndAccommodationBookingPlatform.Application.Queries.BookingQueries;
using TravelAndAccommodationBookingPlatform.Core.DomainMessages;
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

            string htmlContent = InvoiceHtmlGenerator.GenerateInvoiceHtml(booking);

            return await _pdfGeneratorService.GeneratePdfFromHtmlAsync(htmlContent);
        }

    }
}