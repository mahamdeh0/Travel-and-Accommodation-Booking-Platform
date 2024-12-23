using AutoMapper;
using TravelAndAccommodationBookingPlatform.Application.Commands.RoomClassCommands;
using TravelAndAccommodationBookingPlatform.Application.DTOs.HotelDtos;
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
            CreateMap<RoomClass, RoomClassManagementResponseDto>()
                .ForMember(dst => dst.ActiveDiscount, options => options.MapFrom(src => src.Discounts.FirstOrDefault()))
                .ForMember(dst => dst.RoomClassId, opt => opt.MapFrom(src => src.Id));

            CreateMap<RoomClass, RoomClassGuestResponseDto>()
                 .ForMember(dst => dst.ActiveDiscount, options => options.MapFrom(src => src.Discounts.FirstOrDefault()))
                 .ForMember(dst => dst.Gallery, options => options.MapFrom(src => src.Gallery.Select(i => i.Path)))
                 .ForMember(dst => dst.RoomClassId, opt => opt.MapFrom(src => src.Id));
            
            CreateMap<PaginatedResult<RoomClass>, PaginatedResult<RoomClassGuestResponseDto>>()
                .ForMember(dst => dst.Items, options => options.MapFrom(src => src.Items));
            CreateMap<PaginatedResult<RoomClass>, PaginatedResult<RoomClassManagementResponseDto>>()
                .ForMember(dst => dst.Items, options => options.MapFrom(src => src.Items));
            CreateMap<RoomClass, HotelFeaturedDealResponseDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Hotel.Name))
            .ForMember(dest => dest.RoomClassName, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.CityName, opt => opt.MapFrom(src => src.Hotel.City.Name))
            .ForMember(dest => dest.Geolocation, opt => opt.MapFrom(src => src.Hotel.Geolocation))
            .ForMember(dest => dest.StarRating, opt => opt.MapFrom(src => src.Hotel.StarRating))
            .ForMember(dest => dest.NightlyRate, opt => opt.MapFrom(src => src.NightlyRate))
            .ForMember(dest => dest.DiscountPercentage, opt => opt.MapFrom(src => src.Discounts.First().Percentage))
            .ForMember(dest => dest.DiscountStartDate, opt => opt.MapFrom(src => src.Discounts.First().StartDate))
            .ForMember(dest => dest.DiscountEndDate, opt => opt.MapFrom(src => src.Discounts.First().EndDate))
            .ForMember(dest => dest.ReviewsRating, opt => opt.MapFrom(src => src.Hotel.ReviewsRating))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Hotel.BriefDescription))
            .ForMember(dest => dest.Thumbnail, opt => opt.MapFrom(src => src.Hotel.Thumbnail == null ? null : src.Hotel.Thumbnail.Path));
        }
    }
}
