using Asp.Versioning;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading;
using TravelAndAccommodationBookingPlatform.Application.DTOs.HotelDtos;
using TravelAndAccommodationBookingPlatform.Application.Queries.HotelQueries;
using TravelAndAccommodationBookingPlatform.Core.DomainValues;
using TravelAndAccommodationBookingPlatform.WebAPI.DTOs.Hotels;

namespace TravelAndAccommodationBookingPlatform.WebAPI.Controllers
{
    [ApiController]
    [Route("api/user/dashboard")]
    [ApiVersion("1.0")]
    [Authorize(Roles = UserRoles.Guest)]
    public class UserDashboardController : ControllerBase
    {
        private readonly ISender _mediator;
        private readonly IMapper _mapper;

        public UserDashboardController(ISender mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        /// <summary>
        /// Retrieves a list of recently visited hotels by the current user.
        /// </summary>
        /// <param name="getRecentlyVisitedHotelsRequestDto">The request data containing the number of hotels to retrieve.</param>
        /// <returns>List of recently visited hotels.</returns>
        /// <response code="200">Returns the list of recently visited hotels.</response>
        /// <response code="400">If the request data is invalid.</response>
        /// <response code="401">If the user is unauthorized.</response>
        /// <response code="404">If the user is not found.</response>
        [HttpGet("recently-visited-hotels")]
        [ProducesResponseType(typeof(IEnumerable<RecentlyVisitedHotelResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<RecentlyVisitedHotelResponseDto>>> GetRecentlyVisitedHotels([FromQuery] GetRecentlyVisitedHotelsRequestDto getRecentlyVisitedHotelsRequestDto)
        {
            var userIdClaim = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier) ?? throw new ArgumentNullException());

            var query = new GetRecentlyVisitedHotelsQuery { GuestId = userIdClaim };
            _mapper.Map(getRecentlyVisitedHotelsRequestDto, query);
            var hotels = await _mediator.Send(query);
            return Ok(hotels);
        }
    }
}
