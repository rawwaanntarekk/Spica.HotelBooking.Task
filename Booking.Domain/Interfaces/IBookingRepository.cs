using DbBooking = Booking.Domain.Entities.Booking;

namespace Booking.Domain.Interfaces
{
    public interface IBookingRepository
    {
        Task<IReadOnlyList<DbBooking>> GetAllBookingsAsync(bool withTracking = false);
        Task<DbBooking?> GetBookingByIdAsync(int id);
        Task<int> AddBookingAsync(DbBooking booking);
        Task UpdateBooking(DbBooking booking);
        void DeleteBooking(DbBooking booking);
        Task<bool> IsRoomBooked(DbBooking booking);
    }
}
