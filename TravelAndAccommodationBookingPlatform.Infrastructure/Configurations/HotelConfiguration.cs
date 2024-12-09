using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using TravelAndAccommodationBookingPlatform.Core.Entities;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TravelAndAccommodationBookingPlatform.Core.Enums;

namespace TravelAndAccommodationBookingPlatform.Infrastructure.Configurations
{
    public class HotelConfiguration : IEntityTypeConfiguration<Hotel>
    {
        public void Configure(EntityTypeBuilder<Hotel> builder)
        {
            builder.HasKey(h => h.Id);

            builder.Ignore(h => h.FullView);

            builder.Ignore(h => h.SmallPreview);

            builder.Property(h => h.Status).HasConversion(new EnumToStringConverter<HotelStatus>());

            builder.Property(h => h.ReviewsRating).HasPrecision(8, 6);

            builder.HasMany(rc => rc.RoomClasses).WithOne(h => h.Hotel).IsRequired()
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(b => b.Bookings).WithOne(h => h.Hotel).IsRequired()
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
