using AutoMapper;
using TravelAndAccommodationBookingPlatform.Application.Commands.CityCommands;
using TravelAndAccommodationBookingPlatform.Application.Commands.HotelCommands;
using TravelAndAccommodationBookingPlatform.Application.Commands.RoomClassCommands;
using TravelAndAccommodationBookingPlatform.WebAPI.DTOs.Images;

namespace TravelAndAccommodationBookingPlatform.WebAPI.Mapping
{
    public class ImageProfile : Profile
    {
        public ImageProfile()
        {
            CreateMap<ImageCreationRequestDto, AddGalleryToHotelCommand>();
            CreateMap<ImageCreationRequestDto, AddGalleryToRoomClassCommand>();
            CreateMap<ImageCreationRequestDto, AddCityThumbnailCommand>();
            CreateMap<ImageCreationRequestDto, AddHotelThumbnailCommand>();
        }
    }
}