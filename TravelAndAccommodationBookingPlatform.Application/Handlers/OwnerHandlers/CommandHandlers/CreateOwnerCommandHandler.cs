using AutoMapper;
using MediatR;
using TravelAndAccommodationBookingPlatform.Application.Commands.OwnerCommands;
using TravelAndAccommodationBookingPlatform.Application.DTOs.OwnerDtos;
using TravelAndAccommodationBookingPlatform.Core.Entities;
using TravelAndAccommodationBookingPlatform.Core.Interfaces.Repositories;
using TravelAndAccommodationBookingPlatform.Core.Interfaces.UnitOfWork;

namespace TravelAndAccommodationBookingPlatform.Application.Handlers.OwnerHandlers.CommandHandlers
{
    public class CreateOwnerCommandHandler : IRequestHandler<CreateOwnerCommand, OwnerResponseDto>
    {
        private readonly IMapper _mapper;
        private readonly IOwnerRepository _ownerRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateOwnerCommandHandler(IMapper mapper, IOwnerRepository ownerRepository, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _ownerRepository = ownerRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<OwnerResponseDto> Handle(CreateOwnerCommand request, CancellationToken cancellationToken)
        {
            var ownerEntity = _mapper.Map<Owner>(request);
            var createdOwner = await _ownerRepository.AddOwnerAsync(ownerEntity);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<OwnerResponseDto>(createdOwner);
        }
    }
}
