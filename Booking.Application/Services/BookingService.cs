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
        public void CancelBooking(DbBooking booking)
        => repository.DeleteBooking(booking);


        public Task EditBooking(DbBooking booking)
        {
           return repository.UpdateBooking(booking);
        }

        Task IBookingService.CreateBooking(DbBooking booking)
        {
            return CreateBooking(booking);
        }
      
    }
}
