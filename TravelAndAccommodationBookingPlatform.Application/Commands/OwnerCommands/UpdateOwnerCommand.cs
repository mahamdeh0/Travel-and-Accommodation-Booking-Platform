using MediatR;

namespace TravelAndAccommodationBookingPlatform.Application.Commands.OwnerCommands
{
    public class UpdateOwnerCommand : IRequest
    {
        public Guid OwnerId { get; init; }
        public string FirstName { get; init; }
        public string LastName { get; init; }
        public string PhoneNumber { get; init; }
        public string Email { get; init; }
    }
}
