using Booking.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using DbBooking = Booking.Domain.Entities.Booking;

namespace Booking.Infrastructure.Data.Repositories
{
    public class BookingRepository(AppDbContext context) : IBookingRepository
    {
        public async Task<IEnumerable<DbBooking>> GetAllBookingsAsync(bool withTracking = false)
        {
            return withTracking ?
                   await context.Bookings.ToListAsync()
                 : await context.Bookings.AsNoTracking().ToListAsync();
        }


        public async Task<DbBooking?> GetBookingByIdAsync(int id)
        => await context.Bookings.FindAsync(id);

        public async Task AddBookingAsync(DbBooking booking)
        => await context.Bookings.AddAsync(booking);

        public void UpdateBooking(DbBooking booking)
        =>  context.Bookings.Update(booking);

        public void DeleteBooking(DbBooking booking)
        => context.Bookings.Remove(booking);




    }
}
