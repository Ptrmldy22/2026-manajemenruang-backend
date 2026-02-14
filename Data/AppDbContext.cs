using Microsoft.EntityFrameworkCore;
using _2026_manajemenruang_backend.Models;

namespace _2026_manajemenruang_backend.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Ruangan> Ruangans { get; set; }
        public DbSet<Peminjaman> Peminjaman { get; set; }

    }
}
