using AutoMapper;
using TravelAndAccommodationBookingPlatform.Application.Commands.RoomCommands;
using TravelAndAccommodationBookingPlatform.Application.Queries.RoomQueries;
using TravelAndAccommodationBookingPlatform.WebAPI.DTOs.Rooms;
using TravelAndAccommodationBookingPlatform.WebAPI.Helpers;

namespace TravelAndAccommodationBookingPlatform.WebAPI.Mapping
{
    public class RoomProfile : Profile
    {
        public RoomProfile()
        {
            CreateMap<RoomCreationRequestDto, CreateRoomCommand>();
            CreateMap<RoomUpdateRequestDto, UpdateRoomCommand>();
            CreateMap<GetRoomsRequestDto, GetRoomsManagementQuery>()
              .ForMember(
                    dst => dst.OrderDirection,
                    opt => opt.MapFrom(src => MappingHelpers.MapOrderDirection(src.OrderDirection))); CreateMap<GetRoomsGuestsRequestDto, GuestRoomsByClassIdQuery>();
        }
    }
}
