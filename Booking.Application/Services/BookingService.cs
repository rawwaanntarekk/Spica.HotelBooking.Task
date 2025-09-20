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

        public async Task<(bool success, string? ErrorMessage, BookingDTO? bookingDTO)> GetBookingById(int id)
        {
            var booking = await repository.GetBookingByIdAsync(id);

            if (booking == null)
                return (false, "Booking not found.", null);

            var bookingDTO = new BookingDTO
            {
                GuestName = booking.GuestName,
                CheckInDate = booking.CheckInDate,
                CheckOutDate = booking.CheckOutDate,
                BookingDate = booking.BookingDate,
                RoomId = booking.RoomId
            };
            return (true, null, bookingDTO);

        }
 

        private bool IsValidBookingDate(DateTime CheckInDate, DateTime CheckOutDate) => CheckOutDate > CheckInDate;
        private async Task<bool> IsRoomAvailable(int roomId, DateTime checkIn, DateTime checkOut)
        {
            // Is there is any booking for this room that overlaps with the requested dates?

            // 1. Getting all bookings for the room
            var roomBookings = await repository.GetBookingsForRoomAsync(roomId);

            // 2. Checking for overlaps
            return !roomBookings.Any(b => b.CheckInDate < checkOut && b.CheckOutDate > checkIn);
        }

        public async Task<(bool Success, string Message)> CreateBooking(DbBooking booking)
        {
            if (!IsValidBookingDate(booking.CheckInDate, booking.CheckOutDate))
                return (false, "The provided check-in/check-out dates are not valid.");

            if (!await IsRoomAvailable(booking.RoomId, booking.CheckInDate, booking.CheckOutDate))
                return (false, "The room is already booked for the selected dates.");

            await repository.AddBookingAsync(booking);
            return (true, "Booking created successfully.");

        }
        public async Task<(bool Success, string Message)> CancelBooking(int id)
        {
            var result = await GetBookingById(id);
            if (!result.success)
                return (false, "Booking not found.");

            var booking = new DbBooking
            {
                Id = id,
                RoomId = result.bookingDTO?.RoomId ?? 0,
                GuestName = result.bookingDTO?.GuestName ?? string.Empty,
                CheckInDate = result.bookingDTO?.CheckInDate ?? DateTime.MinValue,
                CheckOutDate = result.bookingDTO?.CheckOutDate ?? DateTime.MinValue,
                BookingDate = result.bookingDTO?.BookingDate ?? DateTime.MinValue
            };

            repository.DeleteBooking(booking);
            return (true, "Booking canceled successfully.");
        }


        public async Task<(bool Success, string? ErrorMessage)> EditBooking(int id, BookingUpdateDTO booking)
        {
            if (!IsValidBookingDate(booking.CheckInDate, booking.CheckOutDate))
                return (false, "The provided check-in/check-out dates are not valid.");

            var (success, ErrorMessage, bookingDTO) = await GetBookingById(id);
            if (!success)
                return (false, "Booking not found.");

            if (!await IsRoomAvailable(bookingDTO?.RoomId ?? 0, booking.CheckInDate, booking.CheckOutDate))
                return (false, "The room is already booked for the selected dates.");

            await repository.UpdateBookingDatesAsync(id, booking.CheckInDate, booking.CheckOutDate);

            return (true, "Dates updated successfully.");
        }





    }
}
