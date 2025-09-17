using Microsoft.EntityFrameworkCore;
using DbBooking = Booking.Domain.Entities.Booking;

namespace Booking.Infrastructure.Data.Config
{
    public class BookingConfiguration : IEntityTypeConfiguration<DbBooking>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<DbBooking> builder)
        {
            builder.HasOne(booking => booking.Room)
                   .WithMany()
                   .HasForeignKey(booking => booking.RoomId);

            builder.Property(booking => booking.GuestName)
                   .IsRequired();

        }
    }
}
