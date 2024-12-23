using MediatR;
using TravelAndAccommodationBookingPlatform.Application.Commands.CityCommands;
using TravelAndAccommodationBookingPlatform.Core.DomainMessages;
using TravelAndAccommodationBookingPlatform.Core.Enums;
using TravelAndAccommodationBookingPlatform.Core.Exceptions;
using TravelAndAccommodationBookingPlatform.Core.Interfaces.Repositories;
using TravelAndAccommodationBookingPlatform.Core.Interfaces.UnitOfWork;

namespace TravelAndAccommodationBookingPlatform.Application.Handlers.CityHandlers.CommandHandlers
{
    public class AddCityThumbnailCommandHandler : IRequestHandler<AddCityThumbnailCommand>
    {
        private readonly IImageRepository _imageRepository;
        private readonly ICityRepository _cityRepository;
        private readonly IUnitOfWork _unitOfWork;

        public AddCityThumbnailCommandHandler(IImageRepository imageRepository, ICityRepository cityRepository, IUnitOfWork unitOfWork)
        {
            _imageRepository = imageRepository;
            _cityRepository = cityRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(AddCityThumbnailCommand request, CancellationToken cancellationToken)
        {
            if (!await _cityRepository.ExistsAsync(c => c.Id == request.CityId))
                throw new NotFoundException(CityMessages.CityNotFound);

            await _imageRepository.RemoveImageAsync(request.CityId,ImageType.Thumbnail);
            await _imageRepository.UploadImageAsync(request.Image, request.CityId, ImageType.Thumbnail);

            await _unitOfWork.SaveChangesAsync();
        }
    }
}
