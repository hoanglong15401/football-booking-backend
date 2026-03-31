using FootballBooking.Api.Data;
using FootballBooking.Api.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace FootballBooking.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UsersController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/users
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var today = DateOnly.FromDateTime(DateTime.Today);

            var users = await _context.Users
                .Include(u => u.Bookings)
                .Select(u => new
                {
                    id = u.Id,
                    name = u.FullName,
                    email = u.Email,
                    phone = u.PhoneNumber,
                    role = u.Role,

                    bookingCount = u.Bookings.Count(),

                    activeBookings = u.Bookings.Count(b =>
                        b.BookingDate >= today)
                })
                .ToListAsync();

            return Ok(users);
        }

        // GET: api/users/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
                return NotFound();

            return Ok(user);
        }

        // POST: api/users
        [HttpPost]
        public async Task<IActionResult> Create(User user)
        {
            user.Id = Guid.NewGuid();

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return Ok(user);
        }

        // PUT: api/users/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, User updatedUser)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
                return NotFound();

            user.FullName = updatedUser.FullName;
            user.Email = updatedUser.Email;
            user.PhoneNumber = updatedUser.PhoneNumber;

            await _context.SaveChangesAsync();

            return Ok(user);
        }


        // DELETE: api/users/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
                return NotFound();

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
