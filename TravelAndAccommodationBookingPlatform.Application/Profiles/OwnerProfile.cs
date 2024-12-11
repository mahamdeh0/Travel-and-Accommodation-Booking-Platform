using AutoMapper;
using TravelAndAccommodationBookingPlatform.Application.Commands.OwnerCommands;
using TravelAndAccommodationBookingPlatform.Application.DTOs.OwnerDtos;
using TravelAndAccommodationBookingPlatform.Core.Entities;

namespace TravelAndAccommodationBookingPlatform.Application.Profiles
{
    public class OwnerProfile : Profile
    {
        public OwnerProfile()
        {
            CreateMap<Owner, OwnerResponseDto>();
            CreateMap<CreateOwnerCommand, Owner>();
        }
    }
}
