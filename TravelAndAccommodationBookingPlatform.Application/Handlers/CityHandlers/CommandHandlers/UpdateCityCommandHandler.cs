using AutoMapper;
using MediatR;
using TravelAndAccommodationBookingPlatform.Application.Commands.CityCommands;
using TravelAndAccommodationBookingPlatform.Core.DomainMessages;
using TravelAndAccommodationBookingPlatform.Core.Exceptions;
using TravelAndAccommodationBookingPlatform.Core.Interfaces.Repositories;
using TravelAndAccommodationBookingPlatform.Core.Interfaces.UnitOfWork;

namespace TravelAndAccommodationBookingPlatform.Application.Handlers.CityHandlers.CommandHandlers
{
    public class UpdateCityCommandHandler : IRequestHandler<UpdateCityCommand>
    {
        private readonly ICityRepository _cityRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateCityCommandHandler(ICityRepository cityRepository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _cityRepository = cityRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(UpdateCityCommand request, CancellationToken cancellationToken)
        {
            if (await _cityRepository.ExistsAsync(c => c.PostOffice == request.PostOffice))
                throw new DuplicateCityPostOfficeException(CityMessages.CityWithPostalCodeExists);

            var city = await _cityRepository.GetByIdAsync(request.CityId) ?? throw new NotFoundException(CityMessages.CityNotFound);
            _mapper.Map(request, city);
            await _cityRepository.UpdateAsync(city);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}