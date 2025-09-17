using Booking.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Booking.Infrastructure.Data.Config
{
    public class HotelConfigurations : IEntityTypeConfiguration<Hotel>
    {
        public void Configure(EntityTypeBuilder<Hotel> builder)
        {
            builder.Property(hotel => hotel.Name)
                  .IsRequired()
                  .HasMaxLength(200);

            builder.Property(hotel => hotel.City)
                    .IsRequired();

            builder.Property(hotel => hotel.Stars)
                   .IsRequired()
                   .HasConversion<int>();

            builder.HasMany(hotel => hotel.Rooms)
                    .WithOne()
                    .HasForeignKey(room => room.HotelId)
                    .OnDelete(DeleteBehavior.Cascade);




        }
    }
}
