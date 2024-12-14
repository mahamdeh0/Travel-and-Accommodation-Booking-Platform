using AutoMapper;
using TravelAndAccommodationBookingPlatform.Application.Commands.RoomClassCommands;
using TravelAndAccommodationBookingPlatform.Core.Entities;

namespace TravelAndAccommodationBookingPlatform.Application.Profiles
{
    public class RoomClassProfile : Profile
    {
        public RoomClassProfile()
        {
            CreateMap<CreateRoomClassCommand, RoomClass>();
        }
    }
}
