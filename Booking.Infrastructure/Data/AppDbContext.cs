using Booking.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using DbBooking = Booking.Domain.Entities.Booking;


namespace Booking.Infrastructure.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<DbBooking> Bookings { get; set; }


    }
    
}
