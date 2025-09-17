using DbBooking = Booking.Domain.Entities.Booking;

namespace Booking.Application.Services.Contracts
{
    public interface IBookingService
    {
        public Task<IReadOnlyList<DbBooking>> ListBookings();
        public Task CreateBooking(DbBooking booking);
        public Task EditBooking(DbBooking booking);
        public void CancelBooking(DbBooking booking);



    }
}
