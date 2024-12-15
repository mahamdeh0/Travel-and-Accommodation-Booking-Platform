using AutoMapper;
using TravelAndAccommodationBookingPlatform.Application.Commands.CityCommands;
using TravelAndAccommodationBookingPlatform.Application.DTOs.CityDtos;
using TravelAndAccommodationBookingPlatform.Core.Entities;
using TravelAndAccommodationBookingPlatform.Core.Models;

namespace TravelAndAccommodationBookingPlatform.Application.Profiles
{
    public class CityProfile : Profile
    {
        public CityProfile()
        {
            CreateMap<CreateCityCommand, City>();
            CreateMap<City, CityResponseDto>();
            CreateMap<UpdateCityCommand, City>();
            CreateMap<PaginatedResult<CityManagementDto>, PaginatedResult<CityManagementResponseDto>>().
                ForMember(dst => dst.Items, options => options.MapFrom(src => src.Items)); ;
            CreateMap<CityManagementDto, CityManagementResponseDto>();
            CreateMap<City, TrendingCityResponseDto>()
              .ForMember(dst => dst.Thumbnail, options => options.MapFrom(src => src.Thumbnail != null ? src.Thumbnail.Path : null));
        }
    }
}
