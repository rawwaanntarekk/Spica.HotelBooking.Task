using Booking.Domain.Entities;
using Booking.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Booking.Infrastructure.Data.Repositories
{
    public class HotelRepository(AppDbContext context) : IHotelRepository
    {
        public async Task<IReadOnlyList<Hotel>> GetHotels(string? city = "")
        {
            if (string.IsNullOrWhiteSpace(city?.ToLower()))
                return await context.Hotels.Include(h => h.Rooms).ToListAsync();

            return await context.Hotels
                .Where(h => h.City == city)
                .Include(h => h.Rooms)
                .ToListAsync();
        }
    }
}
