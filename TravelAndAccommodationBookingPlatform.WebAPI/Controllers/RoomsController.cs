using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using TravelAndAccommodationBookingPlatform.Application.Commands.RoomCommands;
using TravelAndAccommodationBookingPlatform.Application.DTOs.RoomDtos;
using TravelAndAccommodationBookingPlatform.Application.Queries.RoomQueries;
using TravelAndAccommodationBookingPlatform.Core.DomainValues;
using TravelAndAccommodationBookingPlatform.WebAPI.DTOs.Rooms;

namespace TravelAndAccommodationBookingPlatform.WebAPI.Controllers
{
    [ApiController]
    [Route("api/room-classes/{roomClassId:guid}/rooms")]
    [ApiVersion("1.0")]
    [Authorize(Roles = UserRoles.Admin)]
    public class RoomsController : ControllerBase
    {
        private readonly ISender _mediator;
        private readonly IMapper _mapper;

        public RoomsController(ISender mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        /// <summary>
        /// Retrieves available rooms for guests within a room class.
        /// </summary>
        /// <param name="roomClassId">The ID of the room class to retrieve available rooms for.</param>
        /// <param name="getRoomsGuestsRequestDto">Query parameters for availability and pagination.</param>
        /// <returns>Returns a list of available rooms for guests.</returns>
        [HttpGet("availableRooms")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<RoomGuestResponseDto>>> GetRoomsGuests(Guid roomClassId, [FromQuery] GetRoomsGuestsRequestDto getRoomsGuestsRequestDto)
        {
            var query = new GuestRoomsByClassIdQuery { RoomClassId = roomClassId };
            _mapper.Map(getRoomsGuestsRequestDto, query);
            var result = await _mediator.Send(query);
            Response.Headers["X-Pagination"] = JsonSerializer.Serialize(result.PaginationMetadata);
            return Ok(result.Items);
        }

        /// <summary>
        /// Retrieves a paginated list of rooms for management within a room class.
        /// </summary>
        /// <param name="roomClassId">The ID of the room class to retrieve rooms for.</param>
        /// <param name="getRoomsRequestDto">Query parameters for filtering, sorting, and pagination.</param>
        /// <returns>Returns a list of rooms for management.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<RoomManagementResponseDto>>> GetRoomsManagement(Guid roomClassId, [FromQuery] GetRoomsRequestDto getRoomsRequestDto)
        {
            var query = new GetRoomsManagementQuery { RoomClassId = roomClassId };
            _mapper.Map(getRoomsRequestDto, query);
            var result = await _mediator.Send(query);
            Response.Headers["X-Pagination"] = JsonSerializer.Serialize(result.PaginationMetadata);
            return Ok(result.Items);
        }

        /// <summary>
        /// Creates a new room within a specific room class.
        /// </summary>
        /// <param name="roomClassId">The ID of the room class to create the room in.</param>
        /// <param name="roomCreationRequestDto">The room creation request data.</param>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> CreateRoom(Guid roomClassId, [FromBody] RoomCreationRequestDto roomCreationRequestDto)
        {
            var command = new CreateRoomCommand { RoomClassId = roomClassId };
            _mapper.Map(roomCreationRequestDto, command);
            await _mediator.Send(command);
            return Created();
        }

        /// <summary>
        /// Updates an existing room within a specific room class.
        /// </summary>
        /// <param name="roomClassId">The ID of the room class.</param>
        /// <param name="id">The ID of the room to update.</param>
        /// <param name="roomUpdateRequestDto">The room update request data.</param>
        [HttpPut("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> UpdateRoom(Guid roomClassId, Guid id, [FromBody] RoomUpdateRequestDto roomUpdateRequestDto)
        {
            var command = new UpdateRoomCommand { RoomClassId = roomClassId, RoomId = id };
            _mapper.Map(roomUpdateRequestDto, command);
            await _mediator.Send(command);
            return NoContent();
        }

        /// <summary>
        /// Deletes a room within a specific room class.
        /// </summary>
        /// <param name="roomClassId">The ID of the room class.</param>
        /// <param name="id">The ID of the room to delete.</param>
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> DeleteRoom(Guid roomClassId, Guid id)
        {
            var command = new DeleteRoomCommand { RoomClassId = roomClassId, RoomId = id };
            await _mediator.Send(command);
            return NoContent();
        }
    }
}
