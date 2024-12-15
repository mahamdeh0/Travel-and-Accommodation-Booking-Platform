using AutoMapper;
using TravelAndAccommodationBookingPlatform.Application.Commands.RoomClassCommands;
using TravelAndAccommodationBookingPlatform.Application.DTOs.RoomClassDtos;
using TravelAndAccommodationBookingPlatform.Core.Entities;
using TravelAndAccommodationBookingPlatform.Core.Models;

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
            CreateMap<PaginatedResult<RoomClass>, PaginatedResult<RoomClassGuestResponseDto>>()
                .ForMember(dst => dst.Items, options => options.MapFrom(src => src.Items));
            CreateMap<PaginatedResult<RoomClass>, PaginatedResult<RoomClassManagementResponseDto>>()
                .ForMember(dst => dst.Items, options => options.MapFrom(src => src.Items));
        }
    }
}
