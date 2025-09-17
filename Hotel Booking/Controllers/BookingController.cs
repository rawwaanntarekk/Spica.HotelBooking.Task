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

            if (result == 0)
                return BadRequest("Room is already booked for the selected dates.");
            else if (result == -1)
                return BadRequest("Check-out date must be after check-in date.");

            return CreatedAtAction(nameof(GetBookings), new { id = result }, booking);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBooking(int id, [FromBody] BookingUpdateDTO updatedBooking)
        {

            var booking = await _bookingService.GetBookingById(id);
            if (booking == null)
                return NotFound(new { message = "Booking not found." });

             await _bookingService.EditBooking(id, updatedBooking);

            return Ok(new { message = "Booking dates updated successfully." });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> CancelBooking(int id)
        {
            var booking = await _bookingService.GetBookingById(id);
            if (booking == null)
                return NotFound(new { message = "Booking not found." });

            var result = _bookingService.CancelBooking(booking);

            return Ok(new { message = "Booking deleted successfully." });
        }

    }

}

