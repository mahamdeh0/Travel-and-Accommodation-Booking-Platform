using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using TravelAndAccommodationBookingPlatform.Core.Entities;

namespace TravelAndAccommodationBookingPlatform.Infrastructure.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.Id);

            builder.HasIndex(u => u.Email).IsUnique();

            builder.HasMany(r => r.Roles)
              .WithMany(u => u.Users)
              .UsingEntity<Dictionary<string, object>>("UserRole", u => u.HasOne<Role>().WithMany()
              .HasForeignKey("RoleId").OnDelete(DeleteBehavior.Cascade), r => r.HasOne<User>().WithMany()
              .HasForeignKey("UserId").OnDelete(DeleteBehavior.Cascade));
        }
    }
}
