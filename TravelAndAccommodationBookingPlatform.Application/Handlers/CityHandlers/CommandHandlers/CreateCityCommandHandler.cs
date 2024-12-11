using AutoMapper;
using MediatR;
using TravelAndAccommodationBookingPlatform.Application.Commands.CityCommands;
using TravelAndAccommodationBookingPlatform.Application.DTOs.CityDtos;
using TravelAndAccommodationBookingPlatform.Core.DomainMessages;
using TravelAndAccommodationBookingPlatform.Core.Entities;
using TravelAndAccommodationBookingPlatform.Core.Exceptions;
using TravelAndAccommodationBookingPlatform.Core.Interfaces.Repositories;
using TravelAndAccommodationBookingPlatform.Core.Interfaces.UnitOfWork;

namespace TravelAndAccommodationBookingPlatform.Application.Handlers.CityHandlers.CommandHandlers
{
    public class CreateCityCommandHandler : IRequestHandler<CreateCityCommand, CityResponseDto>
    {
        private readonly ICityRepository _cityRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public CreateCityCommandHandler(ICityRepository cityRepository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _cityRepository = cityRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<CityResponseDto> Handle(CreateCityCommand request, CancellationToken cancellationToken)
        {
            if (await _cityRepository.ExistsAsync(p => p.PostOffice == request.PostOffice))
                throw new DuplicateCityPostOfficeException(CityMessages.CityWithPostalCodeExists);

            var newCity = _mapper.Map<City>(request);
            var createdCity = await _cityRepository.AddAsync(newCity);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<CityResponseDto>(createdCity);
        }
    }
}
