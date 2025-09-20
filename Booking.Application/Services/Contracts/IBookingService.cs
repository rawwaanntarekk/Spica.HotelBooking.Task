using Booking.Application.DTOs;
using DbBooking = Booking.Domain.Entities.Booking;

namespace Booking.Application.Services.Contracts
{
    public interface IBookingService
    {
        public Task<IReadOnlyList<DbBooking>> ListBookings();
        public Task<(bool success, string? ErrorMessage, BookingDTO? bookingDTO)> GetBookingById(int id);
        public Task<(bool Success, string Message)> CreateBooking(DbBooking booking);
        public Task<(bool Success, string? ErrorMessage)> EditBooking(int id, BookingUpdateDTO booking);
        public Task<(bool Success, string Message)> CancelBooking(int id);



    }
}
