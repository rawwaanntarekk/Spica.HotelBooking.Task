using Booking.Domain.Entities;
using System.Text.Json;

namespace Booking.Infrastructure.Data
{
    public class DbInitializer
    {
        private readonly AppDbContext _context;

        public DbInitializer(AppDbContext context)
        {
            _context = context;
        }

        public async Task SeedAsync()
        {
            // 1. Hotels
            if ( !_context.Hotels.Any())
            {
                var hotelsData = await File.ReadAllTextAsync("../Booking.Infrastructure/Data/Seeds/hotels.json");
                var hotels = JsonSerializer.Deserialize<List<Hotel>>(hotelsData);

                if (hotels != null && hotels.Count != 0)
                {
                    await _context.Hotels.AddRangeAsync(hotels);
                    await _context.SaveChangesAsync();
                }
            }

            // 2. Rooms
            if (!_context.Rooms.Any() && _context.Hotels.Any())
            {
                var roomsData = await File.ReadAllTextAsync("../Booking.Infrastructure/Data/Seeds/rooms.json");
                var rooms = JsonSerializer.Deserialize<List<Room>>(roomsData);
                if (rooms != null && rooms.Count != 0)
                {
                    await _context.Rooms.AddRangeAsync(rooms);
                    await _context.SaveChangesAsync();
                }
            }
        }
    }
}
