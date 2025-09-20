using Booking.Application.DTOs;
using Booking.Application.Services.Contracts;
using Booking.Domain.Interfaces;
using DbBooking = Booking.Domain.Entities.Booking;


namespace Booking.Application.Services
{
    public class BookingService(IBookingRepository repository) : IBookingService
    {

        public async Task<IReadOnlyList<DbBooking>> ListBookings()
        => await repository.GetAllBookingsAsync();

        private int IsValidBookingDate(DateTime CheckInDate, DateTime CheckOutDate) => CheckOutDate > CheckInDate ? 0 : -1;

        public async Task<int> CreateBooking(DbBooking booking)
        {
            if (IsValidBookingDate(booking.CheckInDate, booking.CheckOutDate) != 0)
                return -1;
            if (await repository.IsRoomBooked(booking))
                return 0;
            return await repository.AddBookingAsync(booking);
           
        }
        public int CancelBooking(DbBooking booking) => repository.DeleteBooking(booking);


        public async Task<(bool Success, string? ErrorMessage)> EditBooking(int id, BookingUpdateDTO booking)
        {
            if (IsValidBookingDate(booking.CheckInDate, booking.CheckOutDate) != 0)
                return (false, "The provided check-in/check-out dates are not valid.");


            var entity = await repository.GetBookingByIdAsync(id);
            if (entity == null)
                return (false, "Booking not found.");
            else if (await repository.IsRoomBooked(entity))
                return (false, "The room is already booked for the selected dates.");

            await repository.UpdateBookingDatesAsync(id, booking.CheckInDate, booking.CheckOutDate);

            return (true, "Dates updated successfully.");
        }


        public async Task<DbBooking?> GetBookingById(int id)
        {
            return await repository.GetBookingByIdAsync(id);
        }



    }
}
