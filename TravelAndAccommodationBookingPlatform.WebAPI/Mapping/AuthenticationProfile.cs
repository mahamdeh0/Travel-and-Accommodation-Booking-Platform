using AutoMapper;
using TravelAndAccommodationBookingPlatform.Application.Commands.UserCommands;
using TravelAndAccommodationBookingPlatform.WebAPI.DTOs.Authentication;

namespace TravelAndAccommodationBookingPlatform.WebAPI.Mapping
{
    public class AuthenticationProfile : Profile
    {
        public AuthenticationProfile()
        {
            CreateMap<LoginRequestDto, LoginCommand>();
            CreateMap<RegisterRequestDto, RegisterCommand>();
        }
    }
}
