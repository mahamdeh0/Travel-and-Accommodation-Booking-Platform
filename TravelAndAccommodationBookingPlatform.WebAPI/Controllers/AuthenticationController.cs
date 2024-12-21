using Asp.Versioning;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TravelAndAccommodationBookingPlatform.Application.Commands.UserCommands;
using TravelAndAccommodationBookingPlatform.Application.DTOs.UserDtos;
using TravelAndAccommodationBookingPlatform.Core.DomainValues;
using TravelAndAccommodationBookingPlatform.WebAPI.DTOs.Authentication;

namespace TravelAndAccommodationBookingPlatform.WebAPI.Controllers
{
    /// <summary>
    /// Handles user authentication operations such as Login and Registration.
    /// </summary>
    [ApiController]
    [Route("api/authentication")]
    [ApiVersion("1.0")]
    public class AuthenticationController : ControllerBase
    {
        private readonly ISender _mediator;
        private readonly IMapper _mapper;

        public AuthenticationController(ISender mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        /// <summary>
        /// Registers a new guest user.
        /// </summary>
        /// <param name="loginRequestDto">The login request data containing email and password.</param>
        /// <returns>Returns a JWT token if login is successful.</returns>
        /// <response code="200">Returns the generated JWT token.</response>
        /// <response code="400">If the provided credentials are invalid.</response>
        /// <response code="401">If the user is unauthorized due to invalid credentials.</response>
        [HttpPost("login")]
        [ProducesResponseType(typeof(LoginResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<LoginResponseDto>> Login([FromBody] LoginRequestDto loginRequestDto)
        {
            var loginCommand = _mapper.Map<LoginCommand>(loginRequestDto);
            var result = await _mediator.Send(loginCommand);
            return Ok(result);
        }

        /// <summary>
        /// Processes guest user registration.
        /// </summary>
        /// <param name="registerRequestDto">The registration request data.</param>
        /// <returns>Returns 204 No Content if registration is successful.</returns>
        /// <response code="204">If the registration process is successful.</response>
        /// <response code="400">If the request data is invalid.</response>
        /// <response code="409">If a user with the same email already exists.</response>
        [HttpPost("register-guest")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> RegisterUser(
            [FromBody] RegisterRequestDto registerRequestDto)
        {
            var registerCommand = new RegisterCommand { Role = UserRoles.Guest };
            _mapper.Map(registerRequestDto, registerCommand);
            await _mediator.Send(registerCommand);
            return NoContent();
        }

    }
}
