using MediatR;

namespace TravelAndAccommodationBookingPlatform.Application.Commands.UserCommands
{
    public class RegisterCommand : IRequest
    {
        public string FirstName { get; init; }
        public string LastName { get; init; }
        public string Password { get; init; }
        public string Email { get; init; }
        public DateTime DateOfBirth { get; set; }
        public string PhoneNumber { get; set; }
        public string Role { get; init; }
    }
}
