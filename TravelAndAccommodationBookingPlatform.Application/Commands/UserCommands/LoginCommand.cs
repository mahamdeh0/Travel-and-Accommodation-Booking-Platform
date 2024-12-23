using MediatR;
using TravelAndAccommodationBookingPlatform.Application.DTOs.UserDtos;

namespace TravelAndAccommodationBookingPlatform.Application.Commands.UserCommands
{
    public class LoginCommand : IRequest<LoginResponseDto>
    {
        public string Email { get; init; }
        public string Password { get; init; }
    }
}
