using AutoMapper;
using TravelAndAccommodationBookingPlatform.Application.Commands.HotelCommands;
using TravelAndAccommodationBookingPlatform.Application.DTOs.HotelDtos;
using TravelAndAccommodationBookingPlatform.Core.Entities;
using TravelAndAccommodationBookingPlatform.Core.Models;

namespace TravelAndAccommodationBookingPlatform.Application.Profiles
{
    public class HotelProfile : Profile
    {
        public HotelProfile()
        {
            CreateMap<CreateHotelCommand, Hotel>();
            CreateMap<UpdateHotelCommand, Hotel>();
            CreateMap<HotelManagementDto, HotelManagementResponseDto>().ForMember(dst => dst.Owner, options => options.MapFrom(src => src.Owner));
            CreateMap<Hotel, HotelGuestResponseDto>()
                .ForMember(dest => dest.CountryName, opt => opt.MapFrom(src => src.City.Country))
                .ForMember(dest => dest.CityName, opt => opt.MapFrom(src => src.City.Name))
                .ForMember(dest => dest.Thumbnail, opt => opt.MapFrom(src => src.Thumbnail != null ? src.Thumbnail.Path : null))
                .ForMember(dest => dest.Gallery, opt => opt.MapFrom(src => src.Gallery.Select(image => image.Path)));
            CreateMap<HotelSearchDto, HotelSearchResultResponseDto>()
                .ForMember(dest => dest.BriefDescription, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.CityName, opt => opt.MapFrom(src => src.CityName))
                .ForMember(dest => dest.Thumbnail, opt => opt.MapFrom(src => src.Thumbnail != null ? src.Thumbnail.ToString() : null));
            CreateMap<PaginatedResult<HotelSearchDto>, PaginatedResult<HotelSearchResultResponseDto>>().ForMember(dst => dst.Items, options => options.MapFrom(src => src.Items));
            CreateMap<PaginatedResult<HotelManagementDto>, PaginatedResult<HotelManagementResponseDto>>().ForMember(dst => dst.Items, options => options.MapFrom(src => src.Items));
        }
    }
}
