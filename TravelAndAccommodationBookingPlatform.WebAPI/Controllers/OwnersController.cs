using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using TravelAndAccommodationBookingPlatform.Application.Commands.OwnerCommands;
using TravelAndAccommodationBookingPlatform.Application.DTOs.OwnerDtos;
using TravelAndAccommodationBookingPlatform.Application.Queries.OwnerQueries;
using TravelAndAccommodationBookingPlatform.Core.DomainValues;
using TravelAndAccommodationBookingPlatform.Core.Models;
using TravelAndAccommodationBookingPlatform.WebAPI.DTOs.Owners;


namespace TravelAndAccommodationBookingPlatform.WebAPI.Controllers
{
    /// <summary>
    /// Handles operations related to Owners such as retrieving, creating, and updating owner details.
    /// </summary>
    [ApiController]
    [Route("api/owners")]
    [ApiVersion("1.0")]
    [Authorize(Roles = UserRoles.Admin)]
    public class OwnersController : ControllerBase
    {
        private readonly ISender _mediator;
        private readonly IMapper _mapper;

        public OwnersController(ISender mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        /// <summary>
        /// Retrieves details of a specific owner by their ID.
        /// </summary>
        /// <param name="id">The ID of the owner to retrieve.</param>
        /// <returns>Returns the details of the owner.</returns>
        /// <response code="200">Returns the owner details.</response>
        /// <response code="401">If the user is not authenticated.</response>
        /// <response code="403">If the user is not authorized to access this resource.</response>
        /// <response code="404">If the owner is not found.</response>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<OwnerResponseDto>> GetOwner(Guid id)
        {
            var query = new GetOwnerByIdQuery { OwnerId = id };
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        /// <summary>
        /// Retrieves a paginated list of owners based on the query parameters.
        /// </summary>
        /// <param name="ownersGetRequest">Query parameters for filtering, sorting, and pagination.</param>
        /// <returns>Returns a list of owners with pagination information.</returns>
        /// <response code="200">Returns the list of owners.</response>
        /// <response code="400">If the query parameters are invalid.</response>
        /// <response code="401">If the user is not authenticated.</response>
        /// <response code="403">If the user is not authorized to access this resource.</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<PaginatedResult<OwnerResponseDto>>> GetOwners([FromQuery] GetOwnersRequestDto getOwnersRequestDto)
        {
            var query = _mapper.Map<GetOwnersQuery>(getOwnersRequestDto);
            var result = await _mediator.Send(query);
            Response.Headers["X-Pagination"] = JsonSerializer.Serialize(result.PaginationMetadata);
            return Ok(result.Items);
        }

        /// <summary>
        /// Creates a new owner.
        /// </summary>
        /// <param name="ownerCreationRequestDto">The owner creation request data.</param>
        /// <returns>Returns the location of the created owner.</returns>
        /// <response code="201">If the owner is successfully created.</response>
        /// <response code="400">If the request data is invalid.</response>
        /// <response code="401">If the user is not authenticated.</response>
        /// <response code="403">If the user is not authorized to access this resource.</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> CreateOwner([FromBody] OwnerCreationRequestDto ownerCreationRequestDto)
        {
            var command = _mapper.Map<CreateOwnerCommand>(ownerCreationRequestDto);
            var createdOwner = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetOwner), new { id = createdOwner.Id }, createdOwner);
        }

        /// <summary>
        /// Updates an existing owner.
        /// </summary>
        /// <param name="id">The ID of the owner to update.</param>
        /// <param name="ownerUpdateRequestDto">The updated owner data.</param>
        /// <returns>Returns no content if the update is successful.</returns>
        /// <response code="204">If the owner is successfully updated.</response>
        /// <response code="400">If the request data is invalid.</response>
        /// <response code="401">If the user is not authenticated.</response>
        /// <response code="403">If the user is not authorized to access this resource.</response>
        /// <response code="404">If the owner is not found.</response>
        [HttpPut("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateOwner(Guid id, [FromBody] OwnerUpdateRequestDto ownerUpdateRequestDto)
        {
            var command = new UpdateOwnerCommand { OwnerId = id };
            _mapper.Map(ownerUpdateRequestDto, command);
            await _mediator.Send(command);
            return NoContent();
        }
    }
}
