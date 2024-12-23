using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using TravelAndAccommodationBookingPlatform.Core.Entities;

namespace TravelAndAccommodationBookingPlatform.Infrastructure.Configurations
{
    internal class InvoiceDetailConfiguration : IEntityTypeConfiguration<InvoiceDetail>
    {
        public void Configure(EntityTypeBuilder<InvoiceDetail> builder)
        {
            builder.HasKey(ir => ir.Id);

            builder.Property(ir => ir.PriceAtReservation).HasPrecision(18, 2);

            builder.Property(ir => ir.DiscountAppliedAtBooking).HasPrecision(18, 2);

            builder.Property(ir => ir.AmountAfterDiscount).HasPrecision(18, 2);

            builder.Property(ir => ir.TaxAmount).HasPrecision(18, 2);

            builder.Property(ir => ir.AdditionalCharges).HasPrecision(18, 2);

            builder.Property(ir => ir.TotalAmount).HasPrecision(18, 2);
        }
    }
}
