using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TravelAndAccommodationBookingPlatform.Core.DomainMessages;
using TravelAndAccommodationBookingPlatform.Core.Entities;
using TravelAndAccommodationBookingPlatform.Core.Exceptions;
using TravelAndAccommodationBookingPlatform.Core.Interfaces.Repositories;
using TravelAndAccommodationBookingPlatform.Core.Models;
using TravelAndAccommodationBookingPlatform.Infrastructure.Data;
using TravelAndAccommodationBookingPlatform.Infrastructure.Extensions;

namespace TravelAndAccommodationBookingPlatform.Infrastructure.Repositories
{
    public class HotelRepository : IHotelRepository
    {
        private readonly AppDbContext _context;

        public HotelRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Hotel> AddHotelAsync(Hotel hotel)
        {
            ArgumentNullException.ThrowIfNull(hotel);

            var newHotel = await _context.Hotels.AddAsync(hotel);
            await _context.SaveChangesAsync();

            return newHotel.Entity;
        }

        public async Task RemoveHotelAsync(Guid id)
        {
            var hotel = await _context.Hotels.FindAsync(id);

            if (hotel == null) return;

            _context.Hotels.Remove(hotel);
        }

        public async Task UpdateHotelAsync(Hotel hotel)
        {
            ArgumentNullException.ThrowIfNull(hotel);

            var existingHotel = await _context.Hotels.FindAsync(hotel.Id);
            if (existingHotel == null) return;

            _context.Hotels.Update(hotel);
        }

        public async Task UpdateHotelRatingAsync(Guid id, double newRating)
        {
            var hotel = await _context.Hotels.FindAsync(id);
            if (hotel == null) return;

            hotel.ReviewsRating = newRating;
            _context.Hotels.Update(hotel);
        }

        public async Task<Hotel?> GetHotelByIdAsync(Guid id)
        {
            return await _context.Hotels
                .Include(h => h.City)
                .FirstOrDefaultAsync(h => h.Id == id);
        }

        public async Task<PaginatedResult<HotelManagementDto>> GetHotelsForManagementPageAsync(PaginatedQuery<Hotel> query)
        {
            ArgumentNullException.ThrowIfNull(query);

            var queryable = _context.Hotels
                .Include(h => h.City)
                .Include(h => h.Owner)
                .AsQueryable();

            if (query.FilterExpression != null)
                queryable = queryable.Where(query.FilterExpression);

            if (!string.IsNullOrEmpty(query.SortByColumn))
                queryable = queryable.Sort(query.SortByColumn, query.SortDirection);

            var paginationMetadata = await queryable.GetPaginationMetadataAsync(query.PageNumber, query.PageSize);

            var pagedItems = await queryable
                .GetPage(query.PageNumber, query.PageSize)
                .Select(hotel => new HotelManagementDto
                {
                    Id = hotel.Id,
                    Name = hotel.Name,
                    StarRating = hotel.StarRating,
                    NumberOfRooms = hotel.RoomClasses.Count,
                    CreatedAt = hotel.CreatedAt,
                    UpdatedAt = hotel.UpdatedAt,
                    Owner = hotel.Owner
                })
                .AsNoTracking()
                .ToListAsync();

            return new PaginatedResult<HotelManagementDto>(pagedItems, paginationMetadata);
        }

        public async Task<bool> ExistsByPredicateAsync(Expression<Func<Hotel, bool>> predicate)
        {
            return await _context.Hotels.AnyAsync(predicate);
        }

        public async Task<PaginatedResult<HotelSearchDto>> FindHotelsAsync(PaginatedQuery<Hotel> query)
        {
            ArgumentNullException.ThrowIfNull(query);

            var queryable = _context.Hotels
                .Include(h => h.City)
                .Include(h => h.RoomClasses)
                .AsQueryable();

            if (query.FilterExpression != null)
                queryable = queryable.Where(query.FilterExpression);

            if (!string.IsNullOrEmpty(query.SortByColumn))
                queryable = queryable.Sort(query.SortByColumn, query.SortDirection);

            var paginationMetadata = await queryable.GetPaginationMetadataAsync(query.PageNumber, query.PageSize);

            var pagedItems = await queryable
                .GetPage(query.PageNumber, query.PageSize)
                .Select(hotel => new HotelSearchDto
                {
                    Id = hotel.Id,
                    Name = hotel.Name,
                    StarRating = hotel.StarRating,
                    ReviewsRating = hotel.ReviewsRating,
                    NightlyRate = hotel.RoomClasses.Min(rc => rc.NightlyRate),
                    Description = hotel.BriefDescription
                })
                .AsNoTracking()
                .ToListAsync();

            return new PaginatedResult<HotelSearchDto>(pagedItems, paginationMetadata);
        }

        public async Task UpdateReviewById(Guid id, double rating)
        {
            var hotel = await _context.Hotels.FindAsync(id);
            if (hotel == null)
                throw new NotFoundException(HotelMessages.HotelNotFound);

            hotel.ReviewsRating = rating;
            _context.Hotels.Update(hotel);
        }
    }
}
