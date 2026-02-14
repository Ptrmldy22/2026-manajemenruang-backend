using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace _2026_manajemenruang_backend.Models
{
    public class Peminjaman
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int RuanganId { get; set; }

        [ForeignKey("RuanganId")]
        public Ruangan? Ruangan { get; set; }

        [Required]
        public string NamaPeminjam { get; set; } = string.Empty;

        [Required]
        public DateTime TanggalPinjam { get; set; }

        [Required]
        public DateTime TanggalSelesai { get; set; }

        public string? Keterangan { get; set; }

        public string Status { get; set; } = "Pending";

    }
}
