using AutoMapper;
using TravelAndAccommodationBookingPlatform.Application.Commands.HotelCommands;
using TravelAndAccommodationBookingPlatform.Core.Entities;

namespace TravelAndAccommodationBookingPlatform.Application.Profiles
{
    public class HotelProfile : Profile
    {
        public HotelProfile()
        {
            CreateMap<CreateHotelCommand, Hotel>();
            CreateMap<UpdateHotelCommand, Hotel>();
        }
    }
}
