namespace TravelAndAccommodationBookingPlatform.WebAPI.DTOs.Authentication
{
    public class RegisterRequestDto
    {
        public string FirstName { get; init; }
        public string LastName { get; init; }
        public string Email { get; init; }
        public string Password { get; init; }
        public string PhoneNumber { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}
