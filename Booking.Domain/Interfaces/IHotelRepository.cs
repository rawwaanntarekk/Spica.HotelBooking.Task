using Booking.Domain.Entities;

namespace Booking.Domain.Interfaces
{
    public interface IHotelRepository
    {
        Task<IReadOnlyList<Hotel>> GetHotels(string? city = "");

    }
}
