using AutoMapper;
using TravelAndAccommodationBookingPlatform.Application.Commands.CityCommands;
using TravelAndAccommodationBookingPlatform.Application.DTOs.CityDtos;
using TravelAndAccommodationBookingPlatform.Core.Entities;
using TravelAndAccommodationBookingPlatform.Core.Models;

namespace TravelAndAccommodationBookingPlatform.Application.Profiles
{
    public class CityProfile : Profile
    {
        public CityProfile()
        {
            CreateMap<CreateCityCommand, City>();
            CreateMap<City, CityResponseDto>();
            CreateMap<UpdateCityCommand, City>();
            CreateMap<PaginatedResult<CityManagementDto>, PaginatedResult<CityManagementResponseDto>>()
            CreateMap<CityManagementDto, CityManagementResponseDto>();
        }
    }
}
