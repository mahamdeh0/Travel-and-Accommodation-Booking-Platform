using Asp.Versioning;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using TravelAndAccommodationBookingPlatform.Application.Commands.ReviewCommands;
using TravelAndAccommodationBookingPlatform.Application.DTOs.ReviewsDtos;
using TravelAndAccommodationBookingPlatform.Application.Queries.ReviewQueries;
using TravelAndAccommodationBookingPlatform.Core.DomainValues;
using TravelAndAccommodationBookingPlatform.WebAPI.DTOs.Reviews;

namespace TravelAndAccommodationBookingPlatform.WebAPI.Controllers
{
    [ApiController]
    [Route("api/hotels/{hotelId:guid}/reviews")]
    [ApiVersion("1.0")]
    [Authorize(Roles = UserRoles.Guest)]
    public class ReviewsController : ControllerBase
    {
        private readonly ISender _mediator;
        private readonly IMapper _mapper;

        public ReviewsController(ISender mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        /// <summary>
        /// Retrieves a specific review by ID for a hotel.
        /// </summary>
        /// <param name="hotelId">The ID of the hotel.</param>
        /// <param name="id">The ID of the review to retrieve.</param>
        /// <returns>Returns the review details.</returns>
        [HttpGet("{id:guid}")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ReviewResponseDto>> GetReview(Guid hotelId, Guid id)
        {
            var query = new GetReviewByIdQuery { HotelId = hotelId, ReviewId = id };
            var review = await _mediator.Send(query);
            return Ok(review);
        }

        /// <summary>
        /// Retrieves a paginated list of reviews for a specific hotel.
        /// </summary>
        /// <param name="hotelId">The ID of the hotel to retrieve reviews for.</param>
        /// <param name="getReviewsGetRequestDto">Query parameters for filtering, sorting, and pagination.</param>
        /// <returns>Returns a list of reviews for the hotel.</returns>
        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<ReviewResponseDto>>> GetReviews(Guid hotelId, [FromQuery] GetReviewsRequestDto getReviewsGetRequestDto)
        {
            var query = new GetReviewsByHotelIdQuery { HotelId = hotelId };
            _mapper.Map(getReviewsGetRequestDto, query);
            var result = await _mediator.Send(query);
            Response.Headers["X-Pagination"] = JsonSerializer.Serialize(result.PaginationMetadata);
            return Ok(result.Items);
        }

        /// <summary>
        /// Creates a new review for a specific hotel.
        /// </summary>
        /// <param name="hotelId">The ID of the hotel to create the review for.</param>
        /// <param name="reviewCreationRequestDto">The review creation request data.</param>
        /// <returns>Returns the created review details.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> CreateReview(Guid hotelId, [FromBody] ReviewCreationRequestDto reviewCreationRequestDto)
        {
            var command = new CreateReviewCommand { HotelId = hotelId };
            _mapper.Map(reviewCreationRequestDto, command);
            var createdReview = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetReview), new { hotelId, id = createdReview.Id }, createdReview);
        }

        /// <summary>
        /// Updates an existing review for a specific hotel.
        /// </summary>
        /// <param name="hotelId">The ID of the hotel.</param>
        /// <param name="id">The ID of the review to update.</param>
        /// <param name="reviewUpdateRequestDto">The review update request data.</param>
        [HttpPut("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateReview(Guid hotelId, Guid id, [FromBody] ReviewUpdateRequestDto reviewUpdateRequestDto)
        {
            var command = new UpdateReviewCommand { HotelId = hotelId, ReviewId = id };
            _mapper.Map(reviewUpdateRequestDto, command);
            await _mediator.Send(command);
            return NoContent();
        }

        /// <summary>
        /// Deletes a review for a specific hotel.
        /// </summary>
        /// <param name="hotelId">The ID of the hotel.</param>
        /// <param name="id">The ID of the review to delete.</param>
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteReview(Guid hotelId, Guid id)
        {
            var command = new DeleteReviewCommand { HotelId = hotelId, ReviewId = id };
            await _mediator.Send(command);
            return NoContent();
        }
    }
}
