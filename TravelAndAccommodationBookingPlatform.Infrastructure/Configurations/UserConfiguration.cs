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

            builder.HasData([
                  new User
                  {
                    Id = new Guid("7E754E75-D677-4483-57BD-08DD21B65A13"),
                    FirstName = "Admin",
                    LastName = "Admin",
                    Email = "admin@Test.com",
                    PhoneNumber = "0569345887",
                    Password = "AEO5MEnY6njK7M2UYW6K49qb+MqiU5uGFirzMZ/8d39QAiqJ9S9jdn/Qbe4mnZP4tg=="
                  }]);

            builder.HasMany(u => u.Roles)
                   .WithMany(r => r.Users)
                   .UsingEntity<Dictionary<string, object>>("UserRole", j => j.HasOne<Role>().WithMany()
                   .HasForeignKey("RoleId").OnDelete(DeleteBehavior.Cascade), j => j.HasOne<User>().WithMany()
                   .HasForeignKey("UserId").OnDelete(DeleteBehavior.Cascade))
                   .HasData([new Dictionary<string, object>{["UserId"] = new Guid("7E754E75-D677-4483-57BD-08DD21B65A13"), ["RoleId"] = new Guid("6979DA61-A3BA-42DE-AB1A-08DD21B746D6")}]);
        }
    }
}
