namespace TravelAndAccommodationBookingPlatform.Core.DomainMessages
{
    public static class BookingMessages
    {
        public const string BookingNotFound = "The booking with the provided ID could not be found.";
        public const string NoBookingsForGuestInHotel = "The specified guest has not made any bookings in the specified hotel.";
        public const string BookingNotFoundForGuest = "The booking with the provided ID was not found for the specified guest.";
        public const string InvalidBookingDates = "The provided booking dates are invalid. Please check the check-in and check-out dates.";
        public const string BookingAlreadyCompleted = "The booking has already been completed and cannot be modified or canceled.";
        public const string CannotCancelAfterCheckIn = "The booking with the provided ID cannot be canceled as the check-in date has already passed.";
    }
}
