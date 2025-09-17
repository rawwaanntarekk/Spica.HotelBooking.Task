using Booking.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using DbBooking = Booking.Domain.Entities.Booking;


namespace Booking.Infrastructure.Data
{
    public class AppDbContext: DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
         modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        }


        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<DbBooking> Bookings { get; set; }


    }
    
}
