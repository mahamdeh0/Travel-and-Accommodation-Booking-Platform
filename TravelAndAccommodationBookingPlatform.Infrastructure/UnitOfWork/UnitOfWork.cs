using Microsoft.EntityFrameworkCore;
using TravelAndAccommodationBookingPlatform.Core.Entities;
using TravelAndAccommodationBookingPlatform.Core.Interfaces.UnitOfWork;
using TravelAndAccommodationBookingPlatform.Infrastructure.Data;

namespace TravelAndAccommodationBookingPlatform.Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _dbContext;

        public UnitOfWork(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> SaveChangesAsync()
        {
            _dbContext.ChangeTracker.DetectChanges();
            var entries = _dbContext.ChangeTracker.Entries<IAuditableEntity>();

            foreach (var entry in entries)
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedAt = DateTime.Now;
                }
                else if (entry.State == EntityState.Modified)
                {
                    entry.Entity.UpdatedAt = DateTime.Now;
                }
            }

            return await _dbContext.SaveChangesAsync();
        }
    }
}
