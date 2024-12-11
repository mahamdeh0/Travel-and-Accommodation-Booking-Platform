using AutoMapper;
using TravelAndAccommodationBookingPlatform.Application.DTOs.UserDtos;
using TravelAndAccommodationBookingPlatform.Core.Models;

namespace TravelAndAccommodationBookingPlatform.Application.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<JwtAuthToken, LoginResponseDto>();
        }
    }
}
