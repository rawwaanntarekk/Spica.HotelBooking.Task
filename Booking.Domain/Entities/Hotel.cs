using Booking.Domain.Entities.Common;

namespace Booking.Domain.Entities
{
    public class Hotel : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public int Stars { get; set; }

    }
}
