using MediatR;
using TravelAndAccommodationBookingPlatform.Application.Commands.HotelCommands;
using TravelAndAccommodationBookingPlatform.Core.DomainMessages;
using TravelAndAccommodationBookingPlatform.Core.Enums;
using TravelAndAccommodationBookingPlatform.Core.Exceptions;
using TravelAndAccommodationBookingPlatform.Core.Interfaces.Repositories;
using TravelAndAccommodationBookingPlatform.Core.Interfaces.UnitOfWork;

namespace TravelAndAccommodationBookingPlatform.Application.Handlers.HotelHandlers.CommandHandlers
{
    public class AddGalleryToHotelCommandHandler : IRequestHandler<AddGalleryToHotelCommand>
    {
        private readonly IHotelRepository _hotelRepository;
        private readonly IImageRepository _imageRepository;
        private readonly IUnitOfWork _unitOfWork;

        public AddGalleryToHotelCommandHandler(IHotelRepository hotelRepository, IImageRepository imageRepository, IUnitOfWork unitOfWork)
        {
            _hotelRepository = hotelRepository;
            _imageRepository = imageRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(AddGalleryToHotelCommand request, CancellationToken cancellationToken)
        {
            var hotelExists = await _hotelRepository.ExistsByPredicateAsync(h => h.Id == request.HotelId);
            if (!hotelExists)
                throw new NotFoundException(HotelMessages.HotelNotFound);

            await _imageRepository.UploadImageAsync(request.Image, request.HotelId, ImageType.Gallery);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
