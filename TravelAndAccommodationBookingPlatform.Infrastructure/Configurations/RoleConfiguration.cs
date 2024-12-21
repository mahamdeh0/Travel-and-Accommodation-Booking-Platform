using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TravelAndAccommodationBookingPlatform.Core.Entities;

namespace TravelAndAccommodationBookingPlatform.Infrastructure.Configurations
{
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasKey(r => r.Id);

            builder.HasMany(r => r.Users).WithMany(u => u.Roles);

            builder.HasData([new Role { Id = new Guid("62D4E5FD-F212-4F17-19C8-08DD21B8D161"), Name = "Guest" }, new Role { Id = new Guid("6979DA61-A3BA-42DE-AB1A-08DD21B746D6"), Name = "Admin" }]);
        }
    }
}
