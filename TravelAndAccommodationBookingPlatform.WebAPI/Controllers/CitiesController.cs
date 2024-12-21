using Asp.Versioning;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using TravelAndAccommodationBookingPlatform.Application.Commands.CityCommands;
using TravelAndAccommodationBookingPlatform.Application.DTOs.CityDtos;
using TravelAndAccommodationBookingPlatform.Application.Queries.CityQueries;
using TravelAndAccommodationBookingPlatform.Core.DomainValues;
using TravelAndAccommodationBookingPlatform.WebAPI.DTOs.Cities;
using TravelAndAccommodationBookingPlatform.WebAPI.DTOs.Images;

namespace TravelAndAccommodationBookingPlatform.WebAPI.Controllers
{
    [ApiController]
    [Route("api/cities")]
    [ApiVersion("1.0")]
    [Authorize(Roles = UserRoles.Admin)]
    public class CitiesController : ControllerBase
    {
        private readonly ISender _mediator;
        private readonly IMapper _mapper;

        public CitiesController(ISender mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        /// <summary>
        /// Retrieves a list of trending cities.
        /// </summary>
        /// <param name="GettrendingCitiesRequestDto">Parameters to filter and sort trending cities.</param>
        /// <returns>Returns a list of trending cities.</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet("trending")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<TrendingCityResponseDto>>> GetTrendingCities([FromQuery] GetTrendingCitiesRequestDto getTrendingCitiesRequestDto)
        {
            var query = _mapper.Map<GetTrendingCitiesQuery>(getTrendingCitiesRequestDto);
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        /// <summary>
        /// Retrieves a paginated list of cities for management.
        /// </summary>
        /// <param name="GetcitiesRequestDto">Query parameters for filtering, sorting, and pagination.</param>
        /// <returns>Returns a list of cities with management data.</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CityManagementResponseDto>>> GetCitiesManagement([FromQuery] GetCitiesRequestDto GetcitiesRequestDto)
        {
            var query = _mapper.Map<GetCitiesManagementQuery>(GetcitiesRequestDto);
            var result = await _mediator.Send(query);
            Response.Headers["X-Pagination"] = JsonSerializer.Serialize(result.PaginationMetadata);
            return Ok(result.Items);
        }

        /// <summary>
        /// Creates a new city.
        /// </summary>
        /// <param name="cityCreationRequest">The request data to create a city.</param>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [HttpPost]
        public async Task<IActionResult> CreateCity([FromBody] CityCreationRequestDto cityCreationRequestDto)
        {
            var command = _mapper.Map<CreateCityCommand>(cityCreationRequestDto);
            await _mediator.Send(command);
            return Created();
        }

        /// <summary>
        /// Adds or updates a city thumbnail.
        /// </summary>
        /// <param name="id">The ID of the city to add or update the thumbnail for.</param>
        /// <param name="imageCreationRequestDto">The image data for the thumbnail.</param>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPut("{id:guid}/thumbnail")]
        public async Task<IActionResult> AddCityThumbnail(Guid id, [FromForm] ImageCreationRequestDto imageCreationRequestDto)
        {
            var command = new AddCityThumbnailCommand { CityId = id };
            _mapper.Map(imageCreationRequestDto, command);
            await _mediator.Send(command);
            return NoContent();
        }

        /// <summary>
        /// Updates an existing city.
        /// </summary>
        /// <param name="id">The ID of the city to update.</param>
        /// <param name="cityUpdateRequest">The updated data for the city.</param>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateCity(Guid id, [FromBody] CityUpdateRequestDto cityUpdateRequestDto)
        {
            var command = new UpdateCityCommand { CityId = id };
            _mapper.Map(cityUpdateRequestDto, command);
            await _mediator.Send(command);
            return NoContent();
        }

        /// <summary>
        /// Deletes an existing city.
        /// </summary>
        /// <param name="id">The ID of the city to delete.</param>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteCity(Guid id)
        {
            var command = new DeleteCityCommand { CityId = id };
            await _mediator.Send(command);
            return NoContent();
        }

    }
}
