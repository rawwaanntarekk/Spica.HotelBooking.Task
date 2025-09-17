using Booking.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Booking.Infrastructure.Data.Config
{
    public class RoomConfigurations : IEntityTypeConfiguration<Room>
    {
        public void Configure(EntityTypeBuilder<Room> builder)
        {
          builder.Property(room => room.RoomName)
                 .IsRequired();

            builder.Property(room => room.PricePerNight)
                     .IsRequired()
                     .HasColumnType("decimal(18,2)");
        }
    }
}
