using AutoMapper;
using TravelAndAccommodationBookingPlatform.Application.Commands.ReviewCommands;
using TravelAndAccommodationBookingPlatform.Application.Queries.ReviewQueries;
using TravelAndAccommodationBookingPlatform.WebAPI.DTOs.Reviews;
using TravelAndAccommodationBookingPlatform.WebAPI.Helpers;

namespace TravelAndAccommodationBookingPlatform.WebAPI.Mapping
{
    public class ReviewProfile : Profile
    {
        public ReviewProfile()
        {
            CreateMap<ReviewUpdateRequestDto, UpdateReviewCommand>();
            CreateMap<ReviewCreationRequestDto, CreateReviewCommand>();
            CreateMap<GetReviewsRequestDto, GetReviewsByHotelIdQuery>()
                .ForMember(
                    dst => dst.OrderDirection,
                    opt => opt.MapFrom(src => MappingHelpers.MapOrderDirection(src.OrderDirection))); 
        }
    }
}