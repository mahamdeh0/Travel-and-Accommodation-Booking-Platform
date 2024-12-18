using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using TravelAndAccommodationBookingPlatform.Application.Commands.DiscountCommands;
using TravelAndAccommodationBookingPlatform.Application.DTOs.DiscountDtos;
using TravelAndAccommodationBookingPlatform.Application.Queries.DiscountQueries;
using TravelAndAccommodationBookingPlatform.Core.DomainValues;
using TravelAndAccommodationBookingPlatform.WebAPI.DTOs.Discounts;

namespace TravelAndAccommodationBookingPlatform.WebAPI.Controllers
{
    [ApiController]
    [Route("api/room-classes/{roomClassId:guid}/discounts")]
    [ApiVersion("1.0")]
    [Authorize(Roles = UserRoles.Admin)]
    public class DiscountsController : ControllerBase
    {
        private readonly ISender _mediator;
        private readonly IMapper _mapper;

        public DiscountsController(ISender mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        /// <summary>
        /// Retrieves a specific discount by ID for a room class.
        /// </summary>
        /// <param name="roomClassId">The ID of the room class.</param>
        /// <param name="id">The ID of the discount to retrieve.</param>
        /// <returns>Returns the discount details.</returns>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<DiscountResponseDto>> GetDiscount(Guid roomClassId, Guid id)
        {
            var query = new GetDiscountByIdQuery { RoomClassId = roomClassId, DiscountId = id };
            var discount = await _mediator.Send(query);
            return Ok(discount);
        }

        /// <summary>
        /// Retrieves a paginated list of discounts for a specific room class.
        /// </summary>
        /// <param name="roomClassId">The ID of the room class to retrieve discounts for.</param>
        /// <param name="getDiscountsRequestDto">Query parameters for filtering, sorting, and pagination.</param>
        /// <returns>Returns a list of discounts.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<DiscountResponseDto>>> GetDiscounts(Guid roomClassId, [FromQuery] GetDiscountsRequestDto getDiscountsRequestDto)
        {
            var query = new GetDiscountsQuery { RoomClassId = roomClassId };
            _mapper.Map(getDiscountsRequestDto, query);
            var result = await _mediator.Send(query);
            Response.Headers["X-Pagination"] = JsonSerializer.Serialize(result.PaginationMetadata);
            return Ok(result.Items);
        }

        /// <summary>
        /// Creates a new discount for a specific room class.
        /// </summary>
        /// <param name="roomClassId">The ID of the room class to create a discount for.</param>
        /// <param name="discountCreationRequestDto">The discount creation request data.</param>
        /// <returns>Returns the created discount details.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> CreateDiscount(Guid roomClassId, [FromBody] DiscountCreationRequestDto discountCreationRequestDto)
        {
            var command = new CreateDiscountCommand { RoomClassId = roomClassId };
            _mapper.Map(discountCreationRequestDto, command);
            var createdDiscount = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetDiscount), new { roomClassId, id = createdDiscount.Id }, createdDiscount);
        }

        /// <summary>
        /// Deletes a specific discount by ID for a room class.
        /// </summary>
        /// <param name="roomClassId">The ID of the room class.</param>
        /// <param name="id">The ID of the discount to delete.</param>
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteDiscount(Guid roomClassId, Guid id)
        {
            var command = new DeleteDiscountCommand { RoomClassId = roomClassId, DiscountId = id };
            await _mediator.Send(command);
            return NoContent();
        }
    }
}
