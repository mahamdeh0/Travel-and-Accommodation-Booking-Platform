using AutoMapper;
using TravelAndAccommodationBookingPlatform.Application.Commands.DiscountCommands;
using TravelAndAccommodationBookingPlatform.Application.DTOs.DiscountDtos;
using TravelAndAccommodationBookingPlatform.Core.Entities;
using TravelAndAccommodationBookingPlatform.Core.Models;

namespace TravelAndAccommodationBookingPlatform.Application.Profiles
{
    public class DiscountProfile : Profile
    {
        public DiscountProfile()
        {
            CreateMap<CreateDiscountCommand, Discount>();
            CreateMap<Discount, DiscountResponseDto>();
            CreateMap<PaginatedResult<Discount>, PaginatedResult<DiscountResponseDto>>().ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items));
        }
    }
}
