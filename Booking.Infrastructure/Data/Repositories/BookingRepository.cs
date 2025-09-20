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

        public async Task UpdateBookingDatesAsync(int id, DateTime checkIn, DateTime checkOut)
        {
            var booking = await context.Bookings.FindAsync(id);
            if (booking == null) return;

            booking.CheckInDate = checkIn;
            booking.CheckOutDate = checkOut;

            await context.SaveChangesAsync();
        }

        public int DeleteBooking(DbBooking booking)
        {
            if (context.Bookings.Find(booking.Id) == null)
                return 0;
            context.Bookings.Remove(booking);
            return context.SaveChanges();
        }

        public async Task<IReadOnlyList<DbBooking>> GetBookingsForRoomAsync(int roomId)
        => await context.Bookings.Where(b => b.RoomId == roomId).ToListAsync();


    }
}
