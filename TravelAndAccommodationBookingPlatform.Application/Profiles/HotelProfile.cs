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
        }
    }
}
