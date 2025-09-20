using DbBooking = Booking.Domain.Entities.Booking;

namespace Booking.Domain.Interfaces
{
    public interface IBookingRepository
    {
        Task<IReadOnlyList<DbBooking>> GetAllBookingsAsync(bool withTracking = false);
        Task<DbBooking?> GetBookingByIdAsync(int id);
        Task<int> AddBookingAsync(DbBooking booking);
        Task UpdateBookingDatesAsync(int id, DateTime checkIn, DateTime checkOut);
        int DeleteBooking(DbBooking booking);
        Task<IReadOnlyList<DbBooking>> GetBookingsForRoomAsync(int roomId);
    }
}
