using Booking.Domain.Entities;
using Booking.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using DbBooking = Booking.Domain.Entities.Booking;

namespace Booking.Infrastructure.Data.Repositories
{
    public class BookingRepository(AppDbContext context) : IBookingRepository
    {
        public async Task<IReadOnlyList<DbBooking>> GetAllBookingsAsync(bool withTracking = false)
        {
            return withTracking ?
                   await context.Bookings.ToListAsync()
                 : await context.Bookings.AsNoTracking().ToListAsync();
        }

        public async Task<DbBooking?> GetBookingByIdAsync(int id)
        => await context.Bookings.FindAsync(id);

        public async Task<int> AddBookingAsync(DbBooking booking)
        {
            await context.Bookings.AddAsync(booking);
            return await context.SaveChangesAsync();

        }

        public async Task UpdateBooking(DbBooking booking)
        {
           context.Bookings.Update(booking);
           await context.SaveChangesAsync();

        }

        public void DeleteBooking(DbBooking booking)
        {
            context.Bookings.Remove(booking);
            context.SaveChanges();

        }

        public async Task<bool> IsRoomBooked(DbBooking booking)
        {
            return await context.Bookings.AnyAsync(b =>
            b.RoomId == booking.RoomId &&
            booking.CheckInDate < b.CheckOutDate &&
            booking.CheckOutDate > b.CheckInDate);
        }

     
    }
}
