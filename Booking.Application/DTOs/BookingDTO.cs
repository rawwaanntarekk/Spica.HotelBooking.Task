using Booking.Domain.Entities;

namespace Booking.Application.DTOs
{
    public class BookingDTO
    {
        public string GuestName { get; set; } = string.Empty;
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public DateTime BookingDate { get; set; } = DateTime.UtcNow;
        public int RoomId { get; set; }
    }
}
