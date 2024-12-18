using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using TravelAndAccommodationBookingPlatform.Application.Commands.RoomClassCommands;
using TravelAndAccommodationBookingPlatform.Application.DTOs.RoomClassDtos;
using TravelAndAccommodationBookingPlatform.Application.Queries.RoomClassQueries;
using TravelAndAccommodationBookingPlatform.Core.DomainValues;
using TravelAndAccommodationBookingPlatform.WebAPI.DTOs.Images;
using TravelAndAccommodationBookingPlatform.WebAPI.DTOs.RoomClasses;

namespace TravelAndAccommodationBookingPlatform.WebAPI.Controllers
{
    [ApiController]
    [Route("api/room-classes")]
    [ApiVersion("1.0")]
    [Authorize(Roles = UserRoles.Admin)]
    public class RoomClassesController : ControllerBase
    {
        private readonly ISender _mediator;
        private readonly IMapper _mapper;

        public RoomClassesController(ISender mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        /// <summary>
        /// Retrieves a paginated list of room classes for management.
        /// </summary>
        /// <param name="getRoomClassesRequestDto">Query parameters for filtering, sorting, and pagination.</param>
        /// <returns>Returns a list of room classes for management.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<IEnumerable<RoomClassManagementResponseDto>>> GetRoomClassesManagement([FromQuery] GetRoomClassesRequestDto getRoomClassesRequestDto)
        {
            var query = _mapper.Map<GetRoomClassesManagementQuery>(getRoomClassesRequestDto);
            var result = await _mediator.Send(query);
            Response.Headers["X-Pagination"] = JsonSerializer.Serialize(result.PaginationMetadata);
            return Ok(result.Items);
        }

        /// <summary>
        /// Creates a new room class.
        /// </summary>
        /// <param name="roomClassCreationRequestDto">The room class creation request data.</param>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> CreateRoomClass([FromBody] RoomClassCreationRequestDto roomClassCreationRequestDto)
        {
            var command = _mapper.Map<CreateRoomClassCommand>(roomClassCreationRequestDto);
            await _mediator.Send(command);
            return Created();
        }

        /// <summary>
        /// Adds an image to the gallery of a room class.
        /// </summary>
        /// <param name="id">The ID of the room class.</param>
        /// <param name="imageCreationRequestDto">The image creation request data.</param>
        [HttpPost("{id:guid}/gallery")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> AddGalleryToRoomClass(Guid id, [FromForm] ImageCreationRequestDto imageCreationRequestDto)
        {
            var command = new AddGalleryToRoomClassCommand { RoomClassId = id };
            _mapper.Map(imageCreationRequestDto, command);
            await _mediator.Send(command);
            return NoContent();
        }

        /// <summary>
        /// Updates an existing room class.
        /// </summary>
        /// <param name="id">The ID of the room class to update.</param>
        /// <param name="roomClassUpdateRequestDto">The room class update request data.</param>
        [HttpPut("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> UpdateRoomClass(Guid id, [FromBody] RoomClassUpdateRequestDto roomClassUpdateRequestDto)
        {
            var command = new UpdateRoomClassCommand { RoomClassId = id };
            _mapper.Map(roomClassUpdateRequestDto, command);
            await _mediator.Send(command);
            return NoContent();
        }

        /// <summary>
        /// Deletes a room class by ID.
        /// </summary>
        /// <param name="id">The ID of the room class to delete.</param>
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> DeleteRoomClass(Guid id)
        {
            var command = new DeleteRoomClassCommand { RoomClassId = id };
            await _mediator.Send(command);
            return NoContent();
        }

    }
}
