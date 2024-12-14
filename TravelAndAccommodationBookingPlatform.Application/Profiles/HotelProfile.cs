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
        }
    }
}
