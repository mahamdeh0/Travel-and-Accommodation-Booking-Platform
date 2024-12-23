namespace TravelAndAccommodationBookingPlatform.Core.DomainMessages
{
    public static class UserMessages
    {
        public const string UserNotFound = "No user found with the provided ID.";
        public const string InvalidCredentials = "The provided credentials are incorrect. Please check your email and password.";
        public const string UserNotAuthenticated = "User is not authenticated. Please log in to access this feature.";
        public const string EmailAlreadyExists = "A user with the given email already exists.";
        public const string UserNotGuest = "Authenticated user is not a guest. Only guest users can access this feature.";
        public const string RegistrationSuccessful = "Registration completed successfully. You can now log in.";
        public const string RegistrationFailed = "Registration failed. Please try again later.";
        public const string InvalidRole = "The specified role is invalid. Please check the role assignment.";
    }
}
