using Booking.Application.DTOs;
using DbBooking = Booking.Domain.Entities.Booking;

namespace Booking.Application.Services.Contracts
{
    public interface IBookingService
    {
        public Task<IReadOnlyList<DbBooking>> ListBookings();
        public Task<DbBooking?> GetBookingById(int id);
        public Task<int> CreateBooking(DbBooking booking);
        public Task EditBooking(int id, BookingUpdateDTO booking);
        public int CancelBooking(DbBooking booking);



    }
}
