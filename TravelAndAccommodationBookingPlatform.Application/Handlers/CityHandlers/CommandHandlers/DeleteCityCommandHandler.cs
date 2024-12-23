using MediatR;
using TravelAndAccommodationBookingPlatform.Application.Commands.CityCommands;
using TravelAndAccommodationBookingPlatform.Core.DomainMessages;
using TravelAndAccommodationBookingPlatform.Core.Exceptions;
using TravelAndAccommodationBookingPlatform.Core.Interfaces.Repositories;
using TravelAndAccommodationBookingPlatform.Core.Interfaces.UnitOfWork;

namespace TravelAndAccommodationBookingPlatform.Application.Handlers.CityHandlers.CommandHandlers
{
    public class DeleteCityCommandHandler : IRequestHandler<DeleteCityCommand>
    {
        private readonly ICityRepository _cityRepository;
        private readonly IHotelRepository _hotelRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteCityCommandHandler(ICityRepository cityRepository, IHotelRepository hotelRepository, IUnitOfWork unitOfWork)
        {
            _cityRepository = cityRepository;
            _hotelRepository = hotelRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(DeleteCityCommand request, CancellationToken cancellationToken)
        {
            if (!await _cityRepository.ExistsAsync(c => c.Id == request.CityId))
                throw new NotFoundException(CityMessages.CityNotFound);

            if (await _hotelRepository.ExistsByPredicateAsync(h => h.CityId == request.CityId))
                throw new DependentsExistException(CityMessages.CityHasLinkedEntities);

            await _cityRepository.RemoveAsync(request.CityId);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
