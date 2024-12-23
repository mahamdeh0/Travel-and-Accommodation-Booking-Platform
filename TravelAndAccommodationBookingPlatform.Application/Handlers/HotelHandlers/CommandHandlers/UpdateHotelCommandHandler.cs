using AutoMapper;
using MediatR;
using System.ComponentModel.DataAnnotations;
using TravelAndAccommodationBookingPlatform.Application.Commands.HotelCommands;
using TravelAndAccommodationBookingPlatform.Core.DomainMessages;
using TravelAndAccommodationBookingPlatform.Core.Exceptions;
using TravelAndAccommodationBookingPlatform.Core.Interfaces.Repositories;
using TravelAndAccommodationBookingPlatform.Core.Interfaces.UnitOfWork;

namespace TravelAndAccommodationBookingPlatform.Application.Handlers.HotelHandlers.CommandHandlers
{
    public class UpdateHotelCommandHandler : IRequestHandler<UpdateHotelCommand>
    {
        private readonly ICityRepository _cityRepository;
        private readonly IHotelRepository _hotelRepository;
        private readonly IOwnerRepository _ownerRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateHotelCommandHandler(ICityRepository cityRepository, IHotelRepository hotelRepository, IOwnerRepository ownerRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _cityRepository = cityRepository;
            _hotelRepository = hotelRepository;
            _ownerRepository = ownerRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task Handle(UpdateHotelCommand request, CancellationToken cancellationToken)
        {
            var hotel = await _hotelRepository.GetHotelByIdAsync(request.HotelId);
            if (hotel == null)
                throw new NotFoundException(HotelMessages.HotelNotFound);

            if (!await _ownerRepository.OwnerExistsAsync(o => o.Id == request.OwnerId))
                throw new NotFoundException(OwnerMessages.OwnerNotFound);

            if (await _hotelRepository.ExistsByPredicateAsync(h => h.Geolocation == request.Geolocation))
                throw new ConflictException(HotelMessages.HotelWithSameLocationExists);

            if (!await _cityRepository.ExistsAsync(c => c.Id == request.CityId))
                throw new NotFoundException(CityMessages.CityNotFound);

            if (request.StarRating < 1 || request.StarRating > 5)
                throw new ValidationException(HotelMessages.InvalidStarRating);

            _mapper.Map(request, hotel);

            await _hotelRepository.UpdateHotelAsync(hotel);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
