using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using _2026_manajemenruang_backend.Data;
using _2026_manajemenruang_backend.Models;

namespace _2026_manajemenruang_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeminjamanController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PeminjamanController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Peminjaman>>> GetPeminjaman()
        {
            return await _context.Peminjaman
                .Include(p => p.Ruangan)
                .ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Peminjaman>> GetPeminjaman(int id)
        {
            var peminjaman = await _context.Peminjaman
                .Include(p => p.Ruangan)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (peminjaman == null)
                return NotFound();

            return peminjaman;
        }

        [HttpPost]
        public async Task<ActionResult<Peminjaman>> PostPeminjaman(Peminjaman peminjaman)
        {
            _context.Peminjaman.Add(peminjaman);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPeminjaman), new { id = peminjaman.Id }, peminjaman);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutPeminjaman(int id, Peminjaman peminjaman)
        {
            if (id != peminjaman.Id)
                return BadRequest();

            _context.Entry(peminjaman).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

    }
}
