using AutoMapper;
using MediatR;
using TravelAndAccommodationBookingPlatform.Application.Commands.DiscountCommands;
using TravelAndAccommodationBookingPlatform.Application.DTOs.DiscountDtos;
using TravelAndAccommodationBookingPlatform.Core.DomainMessages;
using TravelAndAccommodationBookingPlatform.Core.Entities;
using TravelAndAccommodationBookingPlatform.Core.Exceptions;
using TravelAndAccommodationBookingPlatform.Core.Interfaces.Repositories;
using TravelAndAccommodationBookingPlatform.Core.Interfaces.UnitOfWork;
using TravelAndAccommodationBookingPlatform.Core.Interfaces.Services;
using System.ComponentModel.DataAnnotations;

namespace TravelAndAccommodationBookingPlatform.Application.Handlers.DiscountHandlers.CommandHandlers
{
    public class CreateDiscountCommandHandler : IRequestHandler<CreateDiscountCommand, DiscountResponseDto>
    {
        private readonly IDiscountRepository _discountRepository;
        private readonly IMapper _mapper;
        private readonly IRoomClassRepository _roomClassRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDateTimeProvider _dateTimeProvider;

        public CreateDiscountCommandHandler(IDiscountRepository discountRepository, IMapper mapper, IRoomClassRepository roomClassRepository, IUnitOfWork unitOfWork, IDateTimeProvider dateTimeProvider)
        {
            _discountRepository = discountRepository;
            _mapper = mapper;
            _roomClassRepository = roomClassRepository;
            _unitOfWork = unitOfWork;
            _dateTimeProvider = dateTimeProvider;
        }

        public async Task<DiscountResponseDto> Handle(CreateDiscountCommand request, CancellationToken cancellationToken)
        {
            if (request.Percentage < 0 || request.Percentage > 100)
                throw new ValidationException(DiscountMessages.InvalidDiscountPercentage);

            if (request.StartDate > request.EndDate)
                throw new ValidationException(DiscountMessages.InvalidDateRange);

            if (!await _roomClassRepository.ExistsAsync(rc => rc.Id == request.RoomClassId))
                throw new NotFoundException(RoomClassMessages.RoomClassNotFound);

            if (await _discountRepository.ExistsAsync(d => d.RoomClassId == request.RoomClassId && request.EndDate >= d.StartDate && request.StartDate <= d.EndDate))
                throw new ConflictException(DiscountMessages.ConflictingDiscountInterval);

            var discount = _mapper.Map<Discount>(request);

            discount.CreatedAt = _dateTimeProvider.Now;

            var createdDiscount = await _discountRepository.AddDiscountAsync(discount);

            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<DiscountResponseDto>(createdDiscount);
        }
    }
}
