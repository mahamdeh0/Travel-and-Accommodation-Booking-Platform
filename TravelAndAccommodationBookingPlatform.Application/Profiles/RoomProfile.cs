using AutoMapper;
using TravelAndAccommodationBookingPlatform.Application.Commands.RoomCommands;
using TravelAndAccommodationBookingPlatform.Application.DTOs.RoomDtos;
using TravelAndAccommodationBookingPlatform.Core.Entities;
using TravelAndAccommodationBookingPlatform.Core.Models;

namespace TravelAndAccommodationBookingPlatform.Application.Profiles
{
    public class RoomProfile : Profile
    {
        public RoomProfile()
        {
            CreateMap<CreateRoomCommand, Room>();
            CreateMap<UpdateRoomCommand, Room>();
            CreateMap<Room, RoomGuestResponseDto>();
            CreateMap<RoomManagementDto, RoomManagementResponseDto>();
            CreateMap<PaginatedResult<RoomManagementDto>, PaginatedResult<RoomManagementResponseDto>>().ForMember(dst => dst.Items, options => options.MapFrom(src => src.Items));
            CreateMap<PaginatedResult<Room>, PaginatedResult<RoomGuestResponseDto>>().ForMember(dst => dst.Items, options => options.MapFrom(src => src.Items));
        }
    }
}
