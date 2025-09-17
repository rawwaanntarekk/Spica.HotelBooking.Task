using Booking.Domain.Entities.Common;

namespace Booking.Domain.Entities
{
    public class Room : BaseEntity
    {
        public string RoomName { get; set; } = string.Empty;
        public decimal PricePerNight { get; set; }
        public int HotelId { get; set; }
    }
}
