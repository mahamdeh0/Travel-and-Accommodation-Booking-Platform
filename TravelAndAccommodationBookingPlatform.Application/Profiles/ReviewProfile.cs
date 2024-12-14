using AutoMapper;
using TravelAndAccommodationBookingPlatform.Application.Commands.ReviewCommands;
using TravelAndAccommodationBookingPlatform.Application.DTOs.ReviewsDtos;
using TravelAndAccommodationBookingPlatform.Core.Entities;

namespace TravelAndAccommodationBookingPlatform.Application.Profiles
{
    public class ReviewProfile : Profile
    {
        public ReviewProfile()
        {
            CreateMap<Review, ReviewResponseDto>();
            CreateMap<UpdateReviewCommand, Review>();
            CreateMap<Review, ReviewResponseDto>();

        }
    }
}
