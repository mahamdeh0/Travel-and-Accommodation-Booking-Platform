using AutoMapper;
using MediatR;
using System.Linq.Expressions;
using TravelAndAccommodationBookingPlatform.Application.DTOs.OwnerDtos;
using TravelAndAccommodationBookingPlatform.Application.Queries.OwnerQueries;
using TravelAndAccommodationBookingPlatform.Core.Entities;
using TravelAndAccommodationBookingPlatform.Core.Enums;
using TravelAndAccommodationBookingPlatform.Core.Interfaces.Repositories;
using TravelAndAccommodationBookingPlatform.Core.Models;

namespace TravelAndAccommodationBookingPlatform.Application.Handlers.OwnerHandlers.QueryHandlers
{
    public class GetOwnersQueryHandler : IRequestHandler<GetOwnersQuery, PaginatedResult<OwnerResponseDto>>
    {
        private readonly IMapper _mapper;
        private readonly IOwnerRepository _ownerRepository;

        public GetOwnersQueryHandler(IOwnerRepository ownerRepository, IMapper mapper)
        {
            _ownerRepository = ownerRepository;
            _mapper = mapper;
        }

        public async Task<PaginatedResult<OwnerResponseDto>> Handle(GetOwnersQuery request, CancellationToken cancellationToken)
        {
            Expression<Func<Owner, bool>> filterExpression = string.IsNullOrEmpty(request.Search)
                 ? _ => true : f => f.FirstName.Contains(request.Search) || f.LastName.Contains(request.Search);

            var query = new PaginatedQuery<Owner>(
                filterExpression,
                request.SortColumn,
                request.PageNumber,
                request.PageSize,
                request.OrderDirection ?? OrderDirection.Ascending
            );

            var owners = await _ownerRepository.GetOwnersAsync(query);
            return _mapper.Map<PaginatedResult<OwnerResponseDto>>(owners);
        }

    }
}
