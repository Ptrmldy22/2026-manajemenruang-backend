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

        // GET: api/Peminjaman
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Peminjaman>>> GetPeminjaman()
        {
            return await _context.Peminjaman
                .Include(p => p.Ruangan)
                .ToListAsync();
        }

        // GET: api/Peminjaman/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Peminjaman>> GetPeminjaman(int id)
        {
            var peminjaman = await _context.Peminjaman
                .Include(p => p.Ruangan)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (peminjaman == null)
                return NotFound("Data peminjaman tidak ditemukan.");

            return peminjaman;
        }

        // POST: api/Peminjaman
        [HttpPost]
        public async Task<ActionResult<Peminjaman>> PostPeminjaman(Peminjaman peminjaman)
        {
            // Validasi ruangan
            var ruangan = await _context.Ruangans.FindAsync(peminjaman.RuanganId);
            if (ruangan == null)
                return BadRequest("Ruangan tidak ditemukan.");

            // Validasi bentrok waktu
            var bentrok = await _context.Peminjaman.AnyAsync(p =>
                p.RuanganId == peminjaman.RuanganId &&
                p.Status != "Rejected" &&
                (
                    peminjaman.TanggalPinjam < p.TanggalSelesai &&
                    peminjaman.TanggalSelesai > p.TanggalPinjam
                )
            );

            if (bentrok)
                return BadRequest("Ruangan sudah dipinjam pada rentang waktu tersebut.");

            // Set default status
            peminjaman.Status = "Pending";

            _context.Peminjaman.Add(peminjaman);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPeminjaman), new { id = peminjaman.Id }, peminjaman);
        }

        // PUT: api/Peminjaman/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPeminjaman(int id, Peminjaman peminjaman)
        {
            if (id != peminjaman.Id)
                return BadRequest("ID tidak sesuai.");

            var existing = await _context.Peminjaman.FindAsync(id);
            if (existing == null)
                return NotFound("Data peminjaman tidak ditemukan.");

            // Validasi ruangan
            var ruangan = await _context.Ruangans.FindAsync(peminjaman.RuanganId);
            if (ruangan == null)
                return BadRequest("Ruangan tidak ditemukan.");

            // Validasi bentrok waktu (kecuali data sendiri)
            var bentrok = await _context.Peminjaman.AnyAsync(p =>
                p.Id != id &&
                p.RuanganId == peminjaman.RuanganId &&
                p.Status != "Rejected" &&
                (
                    peminjaman.TanggalPinjam < p.TanggalSelesai &&
                    peminjaman.TanggalSelesai > p.TanggalPinjam
                )
            );

            if (bentrok)
                return BadRequest("Ruangan sudah dipinjam pada rentang waktu tersebut.");

            // Update data
            existing.RuanganId = peminjaman.RuanganId;
            existing.NamaPeminjam = peminjaman.NamaPeminjam;
            existing.TanggalPinjam = peminjaman.TanggalPinjam;
            existing.TanggalSelesai = peminjaman.TanggalSelesai;
            existing.Keterangan = peminjaman.Keterangan;
            existing.Status = peminjaman.Status;

            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
