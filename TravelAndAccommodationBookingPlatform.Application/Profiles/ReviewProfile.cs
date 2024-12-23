using AutoMapper;
using TravelAndAccommodationBookingPlatform.Application.Commands.ReviewCommands;
using TravelAndAccommodationBookingPlatform.Application.DTOs.ReviewsDtos;
using TravelAndAccommodationBookingPlatform.Core.Entities;
using TravelAndAccommodationBookingPlatform.Core.Models;

namespace TravelAndAccommodationBookingPlatform.Application.Profiles
{
    public class ReviewProfile : Profile
    {
        public ReviewProfile()
        {
            CreateMap<Review, ReviewResponseDto>().ForMember(dest => dest.GuestName, opt => opt.MapFrom(src => src.Guest.FirstName + " " + src.Guest.LastName));
            CreateMap<UpdateReviewCommand, Review>();
            CreateMap<CreateReviewCommand, Review>();
            CreateMap<PaginatedResult<Review>, PaginatedResult<ReviewResponseDto>>().ForMember(dst => dst.Items, options => options.MapFrom(src => src.Items));
        }
    }
}
