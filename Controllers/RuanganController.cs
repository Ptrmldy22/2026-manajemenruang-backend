using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using _2026_manajemenruang_backend.Data;
using _2026_manajemenruang_backend.Models;

namespace _2026_manajemenruang_backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RuanganController : ControllerBase
    {
        private readonly AppDbContext _context;

        public RuanganController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Ruangan>>> GetRuangans()
        {
            return await _context.Ruangans.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Ruangan>> GetRuangan(int id)
        {
            var ruangan = await _context.Ruangans.FindAsync(id);

            if (ruangan == null)
                return NotFound();

            return ruangan;
        }

        [HttpPost]
        public async Task<ActionResult<Ruangan>> CreateRuangan(Ruangan ruangan)
        {
            _context.Ruangans.Add(ruangan);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetRuangan), new { id = ruangan.Id }, ruangan);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRuangan(int id, Ruangan ruangan)
        {
            if (id != ruangan.Id)
                return BadRequest();

            _context.Entry(ruangan).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRuangan(int id)
        {
            var ruangan = await _context.Ruangans.FindAsync(id);

            if (ruangan == null)
                return NotFound();

            _context.Ruangans.Remove(ruangan);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
