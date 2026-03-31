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
    public class FootballFieldsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public FootballFieldsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/footballfields
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var fields = await _context.FootballFields.ToListAsync();
            return Ok(fields);
        }

        // GET: api/footballfields/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var field = await _context.FootballFields.FindAsync(id);

            if (field == null)
                return NotFound();

            return Ok(field);
        }

        // POST: api/footballfields
        [HttpPost]
        public async Task<IActionResult> Create(FootballField field)
        {
            field.Id = Guid.NewGuid();

            _context.FootballFields.Add(field);
            await _context.SaveChangesAsync();

            return Ok(field);
        }

        // PUT: api/footballfields/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, FootballField field)
        {
            var existing = await _context.FootballFields.FindAsync(id);

            if (existing == null)
                return NotFound();

            existing.Name = field.Name;
            existing.Location = field.Location;
            existing.PricePerHour = field.PricePerHour;
            existing.FieldType = field.FieldType;
            existing.OpenTime = field.OpenTime;
            existing.CloseTime = field.CloseTime;
            existing.Description = field.Description;
            existing.IsActive = field.IsActive;

            await _context.SaveChangesAsync();

            return Ok(existing);
        }

        // DELETE: api/footballfields/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var field = await _context.FootballFields.FindAsync(id);

            if (field == null)
                return NotFound();

            _context.FootballFields.Remove(field);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
