using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using TravelAndAccommodationBookingPlatform.Application.Commands.BookingCommands;
using TravelAndAccommodationBookingPlatform.Application.DTOs.BookingDtos;
using TravelAndAccommodationBookingPlatform.Application.Queries.BookingQueries;
using TravelAndAccommodationBookingPlatform.Core.DomainValues;
using TravelAndAccommodationBookingPlatform.WebAPI.DTOs.Bookings;

namespace TravelAndAccommodationBookingPlatform.WebAPI.Controllers
{
    [ApiController]
    [Route("api/user/bookings")]
    [ApiVersion("1.0")]
    [Authorize(Roles = UserRoles.Guest)]
    public class BookingsController : ControllerBase
    {
        private readonly ISender _mediator;
        private readonly IMapper _mapper;

        public BookingsController(ISender mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        /// <summary>
        /// Retrieves a paginated list of bookings for the current user.
        /// </summary>
        /// <param name="getBookingsRequestDto">Query parameters for filtering, sorting, and pagination.</param>
        /// <returns>Returns a list of bookings with pagination.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<IEnumerable<BookingResponseDto>>> GetBookings([FromQuery] GetBookingsRequestDto getBookingsRequestDto)
        {
            var query = _mapper.Map<GetBookingsQuery>(getBookingsRequestDto);
            var result = await _mediator.Send(query);
            Response.Headers["X-Pagination"] = JsonSerializer.Serialize(result.PaginationMetadata);
            return Ok(result.Items);
        }

        /// <summary>
        /// Retrieves a specific booking by ID.
        /// </summary>
        /// <param name="id">The ID of the booking to retrieve.</param>
        /// <returns>Returns the booking details.</returns>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<BookingResponseDto>> GetBooking(Guid id)
        {
            var query = new GetBookingByIdQuery { BookingId = id };
            var booking = await _mediator.Send(query);
            return Ok(booking);
        }

        /// <summary>
        /// Retrieves the invoice for a booking as a PDF.
        /// </summary>
        /// <param name="id">The ID of the booking to retrieve the invoice for.</param>
        /// <returns>Returns the invoice as a PDF file.</returns>
        [HttpGet("{id:guid}/invoice")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<FileResult> GetInvoicePdf(Guid id)
        {
            var query = new RetrieveInvoicePdfQuery { BookingId = id };
            var pdfData = await _mediator.Send(query);
            return File(pdfData, "application/pdf", $"Invoice-{id}.pdf");
        }

        /// <summary>
        /// Creates a new booking.
        /// </summary>
        /// <param name="bookingCreationRequestDto">The booking creation request data.</param>
        /// <returns>Returns the created booking.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> CreateBooking([FromBody] BookingCreationRequestDto bookingCreationRequestDto)
        {
            var command = _mapper.Map<CreateBookingCommand>(bookingCreationRequestDto);
            var createdBooking = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetBooking), new { id = createdBooking.Id }, createdBooking);
        }

        /// <summary>
        /// Deletes an existing booking.
        /// </summary>
        /// <param name="id">The ID of the booking to delete.</param>
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> DeleteBooking(Guid id)
        {
            var command = new DeleteBookingCommand { BookingId = id };
            await _mediator.Send(command);
            return NoContent();
        }

    }
}
