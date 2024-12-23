using Asp.Versioning;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using TravelAndAccommodationBookingPlatform.Application.Commands.HotelCommands;
using TravelAndAccommodationBookingPlatform.Application.DTOs.HotelDtos;
using TravelAndAccommodationBookingPlatform.Application.DTOs.RoomClassDtos;
using TravelAndAccommodationBookingPlatform.Application.Queries.HotelQueries;
using TravelAndAccommodationBookingPlatform.Application.Queries.RoomClassQueries;
using TravelAndAccommodationBookingPlatform.Core.DomainValues;
using TravelAndAccommodationBookingPlatform.WebAPI.DTOs.Hotels;
using TravelAndAccommodationBookingPlatform.WebAPI.DTOs.Images;
using TravelAndAccommodationBookingPlatform.WebAPI.DTOs.RoomClasses;

namespace TravelAndAccommodationBookingPlatform.WebAPI.Controllers
{
    [ApiController]
    [Route("api/hotels")]
    [ApiVersion("1.0")]
    [Authorize(Roles = UserRoles.Admin)]
    public class HotelsController : ControllerBase
    {
        private readonly ISender _mediator;
        private readonly IMapper _mapper;

        public HotelsController(ISender mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        /// <summary>
        /// Get a list of hotels for management.
        /// </summary>
        /// <param name="getHotelsRequestDto">The request DTO for filtering and pagination.</param>
        /// <returns>Paginated list of hotels.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<HotelManagementResponseDto>>> GetHotelsManagement([FromQuery] GetHotelsRequestDto getHotelsRequestDto)
        {
            var query = _mapper.Map<GetHotelManagementQuery>(getHotelsRequestDto);
            var result = await _mediator.Send(query);
            Response.Headers["X-Pagination"] = JsonSerializer.Serialize(result.PaginationMetadata);
            return Ok(result.Items);
        }

        /// <summary>
        /// Get a hotel by ID for guest view.
        /// </summary>
        /// <param name="id">The hotel ID.</param>
        /// <returns>Hotel details for guests.</returns>
        [HttpGet("{id:guid}")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<HotelGuestResponseDto>> GetHotelGuest(Guid id)
        {
            var query = new GetHotelGuestByIdQuery { HotelId = id };
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        /// <summary>
        /// Get featured deals for hotels.
        /// </summary>
        /// <param name="getHotelFeaturedDealsRequestDto">The request DTO for featured deals.</param>
        /// <returns>List of featured deals.</returns>
        [HttpGet("featured-deals")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<HotelFeaturedDealResponseDto>>> GetFeaturedDeals([FromQuery] GetHotelFeaturedDealsRequestDto getHotelFeaturedDealsRequestDto)
        {
            var query = _mapper.Map<GetHotelFeaturedDealsQuery>(getHotelFeaturedDealsRequestDto);
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        /// <summary>
        /// Search and filter hotels.
        /// </summary>
        /// <param name="hotelSearchRequestDto">The request DTO for search criteria.</param>
        /// <returns>List of hotels matching the criteria.</returns>
        [HttpGet("search")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<HotelSearchResultResponseDto>>> SearchHotels([FromQuery] HotelSearchRequestDto hotelSearchRequestDto)
        {
            var query = _mapper.Map<SearchHotelsQuery>(hotelSearchRequestDto);
            var result = await _mediator.Send(query);
            Response.Headers["X-Pagination"] = JsonSerializer.Serialize(result.PaginationMetadata);
            return Ok(result.Items);
        }

        /// <summary>
        /// Get room classes for guests by hotel ID.
        /// </summary>
        /// <param name="id">The hotel ID.</param>
        /// <param name="getRoomClassesGuestRequestDto">The request DTO for room classes.</param>
        /// <returns>List of room classes for guests.</returns>
        [HttpGet("{id:guid}/room-classes")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<RoomClassGuestResponseDto>>> GetRoomClassesGuests(Guid id, [FromQuery] GetRoomClassesGuestRequestDto getRoomClassesGuestRequestDto)
        {
            var query = new GetRoomClassGuestQuery { HotelId = id };
            _mapper.Map(getRoomClassesGuestRequestDto, query);
            var result = await _mediator.Send(query);
            Response.Headers["X-Pagination"] = JsonSerializer.Serialize(result.PaginationMetadata);
            return Ok(result.Items);
        }

        /// <summary>
        /// Create a new hotel.
        /// </summary>
        /// <param name="hotelCreationRequestDto">The request DTO for creating a hotel.</param>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> CreateHotel(HotelCreationRequestDto hotelCreationRequestDto)
        {
            var command = _mapper.Map<CreateHotelCommand>(hotelCreationRequestDto);
            var result = await _mediator.Send(command);
            return Created();
        }

        /// <summary>
        /// Add an image to the hotel's gallery.
        /// </summary>
        /// <param name="id">The hotel ID.</param>
        /// <param name="imageCreationRequestDto">The image creation request DTO.</param>
        /// <returns>No content if successful.</returns>
        [HttpPost("{id:guid}/gallery")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> AddGalleryToHotel(Guid id, [FromForm] ImageCreationRequestDto imageCreationRequestDto)
        {
            var command = new AddGalleryToHotelCommand { HotelId = id };
            _mapper.Map(imageCreationRequestDto, command);
            await _mediator.Send(command);
            return NoContent();
        }

        /// <summary>
        /// Set a thumbnail image for a hotel.
        /// </summary>
        /// <param name="id">The hotel ID.</param>
        /// <param name="imageCreationRequestDto">The image creation request DTO.</param>
        /// <returns>No content if successful.</returns>
        [HttpPut("{id:guid}/thumbnail")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> AddHotelThumbnail(Guid id, [FromForm] ImageCreationRequestDto imageCreationRequestDto)
        {
            var command = new AddHotelThumbnailCommand { HotelId = id };
            _mapper.Map(imageCreationRequestDto, command);
            await _mediator.Send(command);
            return NoContent();
        }

        /// <summary>
        /// Update a hotel by ID.
        /// </summary>
        /// <param name="id">The hotel ID.</param>
        /// <param name="hotelUpdateRequestDto">The request DTO for updating the hotel.</param>
        /// <returns>No content if successful.</returns>
        [HttpPut("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> UpdateHotel(Guid id, HotelUpdateRequestDto hotelUpdateRequestDto)
        {
            var command = new UpdateHotelCommand { HotelId = id };
            _mapper.Map(hotelUpdateRequestDto, command);
            await _mediator.Send(command);
            return NoContent();
        }

        /// <summary>
        /// Delete a hotel by ID.
        /// </summary>
        /// <param name="id">The hotel ID.</param>
        /// <returns>No content if successful.</returns>
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> DeleteHotel(Guid id)
        {
            var command = new DeleteHotelCommand { HotelId = id };
            await _mediator.Send(command);
            return NoContent();
        }

    }
}
