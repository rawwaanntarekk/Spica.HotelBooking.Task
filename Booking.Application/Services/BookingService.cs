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
        
        public async Task<int> CreateBooking(DbBooking booking)
        {
            if (await repository.IsRoomBooked(booking))
                return 0;
            else
                return await repository.AddBookingAsync(booking);
        }
        public int CancelBooking(DbBooking booking)
        {
           return repository.DeleteBooking(booking);
        }


        public async Task EditBooking(int id, BookingUpdateDTO booking)
        {
          await repository.UpdateBookingDatesAsync(id,booking.CheckInDate , booking.CheckOutDate);
        }

        public async Task<DbBooking?> GetBookingById(int id)
        {
            return await repository.GetBookingByIdAsync(id);
        }



    }
}
