using Booking.Domain.Entities.Common;

namespace Booking.Domain.Entities
{
    public class Booking : BaseEntity
    {
        public string GuestName { get; set; } = string.Empty;
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public DateTime BookingDate { get; set; } = DateTime.UtcNow;
        public int RoomId { get; set; }
        public Room Room { get; set; }
    }
}
