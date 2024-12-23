using AutoMapper;
using MediatR;
using TravelAndAccommodationBookingPlatform.Application.Commands.OwnerCommands;
using TravelAndAccommodationBookingPlatform.Core.DomainMessages;
using TravelAndAccommodationBookingPlatform.Core.Exceptions;
using TravelAndAccommodationBookingPlatform.Core.Interfaces.Repositories;
using TravelAndAccommodationBookingPlatform.Core.Interfaces.UnitOfWork;

namespace TravelAndAccommodationBookingPlatform.Application.Handlers.OwnerHandlers.CommandHandlers
{
    public class UpdateOwnerCommandHandler : IRequestHandler<UpdateOwnerCommand>
    {
        private readonly IMapper _mapper;
        private readonly IOwnerRepository _ownerRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateOwnerCommandHandler(IOwnerRepository ownerRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _ownerRepository = ownerRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task Handle(UpdateOwnerCommand request, CancellationToken cancellationToken)
        {
            var owner = await _ownerRepository.GetOwnerByIdAsync(request.OwnerId) ?? throw new NotFoundException(OwnerMessages.OwnerNotFound);
            _mapper.Map(request, owner);
            await _ownerRepository.UpdateOwnerAsync(owner);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}