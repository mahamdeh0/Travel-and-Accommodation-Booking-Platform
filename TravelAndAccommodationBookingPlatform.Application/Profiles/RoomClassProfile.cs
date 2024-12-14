using AutoMapper;
using TravelAndAccommodationBookingPlatform.Application.Commands.RoomClassCommands;
using TravelAndAccommodationBookingPlatform.Application.DTOs.RoomClassDtos;
using TravelAndAccommodationBookingPlatform.Core.Entities;

namespace TravelAndAccommodationBookingPlatform.Application.Profiles
{
    public class RoomClassProfile : Profile
    {
        public RoomClassProfile()
        {
            CreateMap<CreateRoomClassCommand, RoomClass>();
            CreateMap<UpdateRoomClassCommand, RoomClass>();
            CreateMap<RoomClass, RoomClassManagementResponseDto>().ForMember(dst => dst.ActiveDiscount, options => options.MapFrom(src => src.Discounts.FirstOrDefault()));
            CreateMap<RoomClass, RoomClassGuestResponseDto>()
                 .ForMember(dst => dst.ActiveDiscount, options => options.MapFrom(src => src.Discounts.FirstOrDefault()))
                 .ForMember(dst => dst.Gallery, options => options.MapFrom(src => src.Gallery.Select(i => i.Path)));
        }
    }
}
