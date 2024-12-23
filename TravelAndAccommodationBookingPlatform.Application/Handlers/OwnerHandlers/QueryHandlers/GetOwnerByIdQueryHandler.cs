using AutoMapper;
using MediatR;
using TravelAndAccommodationBookingPlatform.Application.DTOs.OwnerDtos;
using TravelAndAccommodationBookingPlatform.Application.Queries.OwnerQueries;
using TravelAndAccommodationBookingPlatform.Core.DomainMessages;
using TravelAndAccommodationBookingPlatform.Core.Exceptions;
using TravelAndAccommodationBookingPlatform.Core.Interfaces.Repositories;

namespace TravelAndAccommodationBookingPlatform.Application.Handlers.OwnerHandlers.QueryHandlers
{
    public class GetOwnerByIdQueryHandler : IRequestHandler<GetOwnerByIdQuery, OwnerResponseDto>
    {
        private readonly IMapper _mapper;
        private readonly IOwnerRepository _ownerRepository;

        public GetOwnerByIdQueryHandler(IOwnerRepository ownerRepository, IMapper mapper)
        {
            _ownerRepository = ownerRepository;
            _mapper = mapper;
        }

        public async Task<OwnerResponseDto> Handle(GetOwnerByIdQuery request, CancellationToken cancellationToken)
        {
            var owner = await _ownerRepository.GetOwnerByIdAsync(request.OwnerId) ?? throw new NotFoundException(OwnerMessages.OwnerNotFound);
            return _mapper.Map<OwnerResponseDto>(owner);
        }
    }
}
