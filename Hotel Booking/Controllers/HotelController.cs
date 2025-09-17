using Booking.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Hotel_Booking.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HotelController(IHotelRepository repository) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult> GetHotels([FromQuery] string? city = "")
        {
            var hotels = await repository.GetHotels(city);
            return Ok(hotels);
        }

    }
}
