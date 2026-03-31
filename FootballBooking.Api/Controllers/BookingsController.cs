using FootballBooking.Api.Data;
using FootballBooking.Api.Entities;
using FootballBooking.Api.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace FootballBooking.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookingsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public BookingsController(AppDbContext context)
        {
            _context = context;
        }

        // =========================================
        // 📌 API: Lấy tất cả booking (Admin)
        // =========================================
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var bookings = await _context.Bookings
                .Include(b => b.FootballField)
                .Include(b => b.User)
                .Select(b => new BookingDto
                {
                    Id = b.Id,
                    CustomerName = b.User.FullName,
                    FieldName = b.FootballField.Name,
                    FieldAddress = b.FootballField.Location,
                    FieldType = b.FootballField.FieldType,

                    BookingDate = b.BookingDate,
                    StartTime = b.StartTime,
                    EndTime = b.EndTime,
                    TotalPrice = b.TotalPrice,

                    Drink = b.Drink
                })
                .ToListAsync();

            return Ok(bookings);
        }

        // =========================================
        // 📌 API: Lấy chi tiết 1 booking
        // =========================================
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var booking = await _context.Bookings
                .Include(b => b.FootballField)
                .Include(b => b.User)
                .FirstOrDefaultAsync(b => b.Id == id);

            if (booking == null)
                return NotFound();

            return Ok(booking);
        }

        // =========================================
        // 📌 API: Tạo booking (FIX DTO)
        // =========================================
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(CreateBookingDto dto)
        {
            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (userId == null)
                    return Unauthorized();

                var field = await _context.FootballFields
                    .FirstOrDefaultAsync(f => f.Id == dto.FootballFieldId);

                if (field == null)
                    return BadRequest("Sân không tồn tại");

                if (!field.IsActive)
                    return BadRequest("Sân đang bị khóa");

                var startTime = dto.StartTime;
                var endTime = startTime.Add(TimeSpan.FromHours(1));

                if (dto.BookingDate < DateOnly.FromDateTime(DateTime.Now))
                    return BadRequest("Không thể đặt ngày trong quá khứ");

                if (dto.BookingDate == DateOnly.FromDateTime(DateTime.Now))
                {
                    var now = DateTime.Now.TimeOfDay;
                    if (startTime <= now)
                        return BadRequest("Không thể đặt giờ trong quá khứ");
                }

                if (startTime < field.OpenTime || endTime > field.CloseTime)
                    return BadRequest("Ngoài giờ hoạt động");

                var isOverlapping = await _context.Bookings.AnyAsync(b =>
                    b.FootballFieldId == dto.FootballFieldId &&
                    b.BookingDate == dto.BookingDate &&
                    startTime < b.EndTime &&
                    endTime > b.StartTime
                );

                if (isOverlapping)
                    return BadRequest("Khung giờ đã được đặt");

                var totalHours = (endTime - startTime).TotalHours;

                var booking = new Booking
                {
                    Id = Guid.NewGuid(),
                    UserId = Guid.Parse(userId),
                    FootballFieldId = dto.FootballFieldId,
                    BookingDate = dto.BookingDate,
                    StartTime = startTime,
                    EndTime = endTime,
                    TotalPrice = (decimal)totalHours * field.PricePerHour,
                    Drink = dto.Drink,
                    CreatedAt = DateTime.UtcNow
                };

                _context.Bookings.Add(booking);
                await _context.SaveChangesAsync();

                // ✔️ RETURN JSON SẠCH
                return Ok(new
                {
                    message = "Đặt sân thành công",
                    booking = new
                    {
                        booking.Id,
                        booking.FootballFieldId,
                        booking.TotalPrice,
                        date = booking.BookingDate.ToString(),
                        startTime = booking.StartTime.ToString(@"hh\:mm"),
                        endTime = booking.EndTime.ToString(@"hh\:mm")
                    }
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Lỗi server",
                    error = ex.Message
                });
            }
        }
        // =========================================
        // 📌 API: Xóa booking
        // =========================================
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var booking = await _context.Bookings.FindAsync(id);

            if (booking == null)
                return NotFound();

            _context.Bookings.Remove(booking);
            await _context.SaveChangesAsync();

            return Ok();
        }

        // =========================================
        // 📌 API: Lấy slot đã đặt
        // =========================================
        [HttpGet("field/{fieldId}")]
        public IActionResult GetBookedSlots(Guid fieldId, DateOnly date)
        {
            var bookings = _context.Bookings
                .Where(b => b.FootballFieldId == fieldId && b.BookingDate == date)
                .Select(b => new
                {
                    time = b.StartTime.ToString(@"hh\:mm")
                })
                .ToList();

            return Ok(bookings);
        }

        // =========================================
        // 📌 API: User xem booking của mình
        // =========================================
        [Authorize]
        [HttpGet("my")]
        public IActionResult GetMyBookings()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userId == null)
                return Unauthorized();

            var guidUserId = Guid.Parse(userId);

            var bookings = _context.Bookings
                .Include(b => b.FootballField)
                .Where(b => b.UserId == guidUserId)
                .Select(b => new
                {
                    b.Id,
                    date = b.BookingDate.ToString(),
                    time = b.StartTime.ToString(@"hh\:mm"),
                    price = b.TotalPrice,
                    fieldName = b.FootballField.Name
                })
                .ToList();

            return Ok(bookings);
        }

        // =========================================
        // 📌 API: Admin xem theo ngày
        // =========================================
        [HttpGet("date")]
        public IActionResult GetBookingsByDate(DateOnly date)
        {
            var bookings = _context.Bookings
                .Include(b => b.FootballField)
                .Include(b => b.User)
                .Where(b => b.BookingDate == date)
                .Select(b => new
                {
                    b.Id,
                    time = b.StartTime.ToString(@"hh\:mm"),
                    fieldName = b.FootballField.Name,
                    userEmail = b.User.Email
                })
                .ToList();

            return Ok(bookings);
        }
    }
}