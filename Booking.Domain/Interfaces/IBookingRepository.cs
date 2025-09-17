using DbBooking = Booking.Domain.Entities.Booking;

namespace Booking.Domain.Interfaces
{
    public interface IBookingRepository
    {
        Task<IEnumerable<DbBooking>> GetAllBookingsAsync(bool withTracking = false);
        Task<DbBooking?> GetBookingByIdAsync(int id);
        Task AddBookingAsync(DbBooking booking);
        void UpdateBooking(DbBooking booking);
        void DeleteBooking(DbBooking booking);
    }
}
