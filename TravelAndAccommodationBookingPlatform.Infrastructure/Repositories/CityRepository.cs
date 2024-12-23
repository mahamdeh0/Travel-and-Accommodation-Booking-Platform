using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TravelAndAccommodationBookingPlatform.Core.DomainMessages;
using TravelAndAccommodationBookingPlatform.Core.Entities;
using TravelAndAccommodationBookingPlatform.Core.Enums;
using TravelAndAccommodationBookingPlatform.Core.Exceptions;
using TravelAndAccommodationBookingPlatform.Core.Interfaces.Repositories;
using TravelAndAccommodationBookingPlatform.Core.Models;
using TravelAndAccommodationBookingPlatform.Infrastructure.Data;
using TravelAndAccommodationBookingPlatform.Infrastructure.Extensions;

namespace TravelAndAccommodationBookingPlatform.Infrastructure.Repositories
{
    public class CityRepository : ICityRepository
    {
        private readonly AppDbContext _context;

        public CityRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<City?> GetByIdAsync(Guid id)
        {
            return await _context.Cities.FindAsync(id);
        }

        public async Task<City> AddAsync(City city)
        {
            ArgumentNullException.ThrowIfNull(city);

            var newCity = await _context.Cities.AddAsync(city);

            return newCity.Entity;
        }

        public async Task RemoveAsync(Guid id)
        {
            var city = await _context.Cities.FindAsync(id);

            if (city != null)
                _context.Cities.Remove(city);
        }

        public async Task UpdateAsync(City city)
        {
            ArgumentNullException.ThrowIfNull(city);

            if (!await _context.Cities.AnyAsync(c => c.Id == city.Id))
                throw new NotFoundException(CityMessages.CityNotFound);

            _context.Cities.Update(city);
        }

        public async Task<bool> ExistsAsync(Expression<Func<City, bool>> predicate)
        {
            return await _context.Cities.AnyAsync(predicate);
        }

        public async Task<PaginatedResult<CityManagementDto>> GetCitiesForAdminAsync(PaginatedQuery<City> query)
        {
            ArgumentNullException.ThrowIfNull(query);

            var queryable = _context.Cities.AsQueryable();

            if (query.FilterExpression != null)
                queryable = queryable.Where(query.FilterExpression);

            if (!string.IsNullOrEmpty(query.SortByColumn))
                queryable = queryable.Sort(query.SortByColumn, query.SortDirection);

            var paginationMetadata = await queryable.GetPaginationMetadataAsync(query.PageNumber, query.PageSize);

            var pagedItems = await queryable
                .GetPage(query.PageNumber, query.PageSize)
                .Select(city => new CityManagementDto
                {
                    Id = city.Id,
                    Name = city.Name,
                    Country = city.Country,
                    PostOffice = city.PostOffice,
                    Region = city.Region,
                    TotalHotels = city.Hotels.Count(),
                    CreatedAt = city.CreatedAt,
                    UpdatedAt = city.UpdatedAt
                })
                .ToListAsync();

            return new PaginatedResult<CityManagementDto>(pagedItems, paginationMetadata);
        }

        public async Task<IEnumerable<City>> GetTopMostVisitedAsync(int count)
        {
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(count);

            var mostVisitedCitiesQuery =
                from booking in _context.Bookings
                group booking by booking.Hotel.CityId into grouped
                orderby grouped.Count() descending
                select new { CityId = grouped.Key, VisitCount = grouped.Count() };

            var mostVisitedCitiesWithThumbnails = await mostVisitedCitiesQuery
                .Take(count)
                .Join(
                    _context.Cities.AsNoTracking(),
                    g => g.CityId,
                    c => c.Id,
                    (g, c) => new { City = c, g.VisitCount }
                )
                .GroupJoin(
                    _context.Images.AsNoTracking().Where(img => img.Type == ImageType.Thumbnail),
                    cityWithVisit => cityWithVisit.City.Id,
                    img => img.EntityId,
                    (cityWithVisit, images) => new
                    {
                        City = cityWithVisit.City,
                        VisitCount = cityWithVisit.VisitCount,
                        Thumbnail = images.FirstOrDefault()
                    }
                )
                .ToListAsync();

            return mostVisitedCitiesWithThumbnails.Select(c =>
                {
                    c.City.Thumbnail = c.Thumbnail;
                    return c.City;
                });
        }
    }
}
