using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TravelAndAccommodationBookingPlatform.Core.Entities;

namespace TravelAndAccommodationBookingPlatform.Infrastructure.Configurations
{
    public class CityConfiguration : IEntityTypeConfiguration<City>
    {
        public void Configure(EntityTypeBuilder<City> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Ignore(c => c.SmallPreview);

            builder.HasMany(h => h.Hotels).WithOne(c => c.City).IsRequired()
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
