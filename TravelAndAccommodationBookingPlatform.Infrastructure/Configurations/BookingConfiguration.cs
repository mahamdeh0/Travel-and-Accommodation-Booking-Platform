using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TravelAndAccommodationBookingPlatform.Core.Entities;
using TravelAndAccommodationBookingPlatform.Core.Enums;

namespace TravelAndAccommodationBookingPlatform.Infrastructure.Configurations
{
    public class BookingConfiguration : IEntityTypeConfiguration<Booking>
    {
        public void Configure(EntityTypeBuilder<Booking> builder)
        {
            builder.HasKey(b => b.Id);

            builder.Property(b => b.TotalPrice).HasColumnType("decimal(18,2)");

            builder.Property(b => b.PaymentType).HasConversion(new EnumToStringConverter<PaymentType>());

            builder.Property(b => b.GuestRemarks).HasMaxLength(500);

            builder.HasMany(b => b.Rooms).WithMany(r => r.Bookings);

            builder.HasMany(b => b.Invoice).WithOne(ir => ir.Booking)
                   .IsRequired()
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
