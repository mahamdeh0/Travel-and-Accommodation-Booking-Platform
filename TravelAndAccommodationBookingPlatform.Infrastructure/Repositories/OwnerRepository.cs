using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TravelAndAccommodationBookingPlatform.Core.DomainMessages;
using TravelAndAccommodationBookingPlatform.Core.Entities;
using TravelAndAccommodationBookingPlatform.Core.Exceptions;
using TravelAndAccommodationBookingPlatform.Core.Interfaces.Repositories;
using TravelAndAccommodationBookingPlatform.Core.Models;
using TravelAndAccommodationBookingPlatform.Infrastructure.Data;
using TravelAndAccommodationBookingPlatform.Infrastructure.Extensions;

namespace TravelAndAccommodationBookingPlatform.Infrastructure.Persistence.Repositories
{
    public class OwnerRepository : IOwnerRepository
    {
        private readonly AppDbContext _context;

        public OwnerRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Owner> AddOwnerAsync(Owner owner)
        {
            ArgumentNullException.ThrowIfNull(owner);

            var newOwnerEntry = await _context.AddAsync(owner);

            return newOwnerEntry.Entity;
        }

        public async Task<PaginatedResult<Owner>> GetOwnersAsync(PaginatedQuery<Owner> query)
        {
            ArgumentNullException.ThrowIfNull(query);

            var queryable = _context.Owners.AsQueryable();

            if (query.FilterExpression != null)
                queryable = queryable.Where(query.FilterExpression);

            if (!string.IsNullOrEmpty(query.SortByColumn))
                queryable = queryable.Sort(query.SortByColumn, query.SortDirection);

            var paginationMetadata = await queryable.GetPaginationMetadataAsync(query.PageNumber, query.PageSize);

            var pagedItems = await queryable
                .GetPage(query.PageNumber, query.PageSize)
                .AsNoTracking()
                .ToListAsync();

            return new PaginatedResult<Owner>(pagedItems, paginationMetadata);
        }

        public async Task UpdateOwnerAsync(Owner owner)
        {
            ArgumentNullException.ThrowIfNull(owner);
            if (!await _context.Owners.AnyAsync(o => o.Id == owner.Id))
                throw new NotFoundException(OwnerMessages.OwnerNotFound);

            _context.Owners.Update(owner);
        }

        public async Task<Owner?> GetOwnerByIdAsync(Guid id)
        {
            return await _context.Owners.FindAsync(id);
        }

        public async Task<bool> OwnerExistsAsync(Expression<Func<Owner, bool>> predicate)
        {
            return await _context.Owners.AnyAsync(predicate);
        }
    }
}
