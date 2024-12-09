using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TravelAndAccommodationBookingPlatform.Core.Entities;
using TravelAndAccommodationBookingPlatform.Core.Enums;

namespace TravelAndAccommodationBookingPlatform.Infrastructure.Configurations
{
    public class RoomClassConfiguration : IEntityTypeConfiguration<RoomClass>
    {
        public void Configure(EntityTypeBuilder<RoomClass> builder)
        {
            builder.HasKey(rc => rc.Id);

            builder.Property(rc => rc.TypeOfRoom).HasConversion(new EnumToStringConverter<RoomType>());

            builder.Property(rc => rc.NightlyRate).HasPrecision(18, 2);

            builder.Ignore(h => h.FullView);

            builder.HasIndex(rc => rc.TypeOfRoom);

            builder.HasIndex(rc => rc.NightlyRate);

            builder.HasMany(rc => rc.Rooms).WithOne(r => r.RoomClass).IsRequired()
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
