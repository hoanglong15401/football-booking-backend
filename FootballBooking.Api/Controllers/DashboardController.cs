using FootballBooking.Api.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace FootballBooking.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DashboardController : ControllerBase
    {
        private readonly AppDbContext _context;

        public DashboardController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var now = DateTime.Now;
            var today = DateOnly.FromDateTime(now);

            var weekStart = today.AddDays(-(int)DateTime.Today.DayOfWeek);
            var monthStart = new DateOnly(today.Year, today.Month, 1);

            var bookings = await _context.Bookings.ToListAsync();

            var todayBookings = bookings.Count(b => b.BookingDate == today);
            var weekBookings = bookings.Count(b => b.BookingDate >= weekStart);
            var monthBookings = bookings.Count(b => b.BookingDate >= monthStart);

            var todayRevenue = bookings
                .Where(b => b.BookingDate == today)
                .Sum(b => b.TotalPrice);

            var weekRevenue = bookings
                .Where(b => b.BookingDate >= weekStart)
                .Sum(b => b.TotalPrice);

            var monthRevenue = bookings
                .Where(b => b.BookingDate >= monthStart)
                .Sum(b => b.TotalPrice);

            var activeFields = bookings.Count(b =>
                b.BookingDate == today &&
                b.StartTime <= now.TimeOfDay &&
                b.EndTime >= now.TimeOfDay
            );

            return Ok(new
            {
                todayBookings,
                weekBookings,
                monthBookings,
                todayRevenue,
                weekRevenue,
                monthRevenue,
                activeFields
            });
        }
    }
}