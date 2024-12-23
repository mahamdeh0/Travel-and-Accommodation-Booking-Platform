namespace TravelAndAccommodationBookingPlatform.Core.DomainMessages
{
    public static class ReviewMessages
    {
        public const string ReviewNotFoundForUserAndHotel = "The review for the specified user and hotel could not be found.";
        public const string ReviewNotFound = "The review with the provided ID could not be found.";
        public const string GuestAlreadyReviewedHotel = "The guest has already submitted a review for the specified hotel.";
        public const string InvalidReviewRating = "The provided rating is invalid. It must be between 1 and 5.";
        public const string ReviewNotFoundForHotel = "The review with the provided ID could not be found for the specified hotel.";
    }
}
