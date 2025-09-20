using Booking.Application.DTOs;
using Booking.Application.Services.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DbBooking = Booking.Domain.Entities.Booking;

namespace Hotel_Booking.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookingController : ControllerBase
    {
        private readonly IBookingService _bookingService;

        public BookingController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DbBooking>>> GetBookings()
        {
            var bookings = await _bookingService.ListBookings();
            return Ok(bookings);
        }



        [HttpPost]
        public async Task<ActionResult<int>> CreateBooking([FromBody] BookingDTO booking)
        {
            var bookingEntity = new DbBooking
            {
                RoomId = booking.RoomId,
                GuestName = booking.GuestName,
                CheckInDate = booking.CheckInDate,
                CheckOutDate = booking.CheckOutDate
            };

            var result = await _bookingService.CreateBooking(bookingEntity);

            if(!result.Success)
                return BadRequest(new { message = result.Message });

            return CreatedAtAction(nameof(GetBookings), new { id = result }, booking);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBooking(int id, [FromBody] BookingUpdateDTO updatedBooking)
        {
            var result = await _bookingService.GetBookingById(id);
            if (!result.success)
                return NotFound(new { message = "Booking not found." });

            var (success, ErrorMessage) = await _bookingService.EditBooking(id, updatedBooking);

            if (!success)
                return BadRequest(new { message = ErrorMessage });

            return Ok(new { message = "Booking dates updated successfully." });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> CancelBooking(int id)
        {
            var result = await _bookingService.GetBookingById(id);

            if (!result.success)
                return NotFound(new { message = "Booking not found." });

            var (success, message) = await _bookingService.CancelBooking(id);

            if (!success)
                return BadRequest(new { message });

            return Ok(new { message = "Booking deleted successfully." });
        }

    }

}

