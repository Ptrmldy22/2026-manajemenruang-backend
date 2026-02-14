using System.ComponentModel.DataAnnotations;

namespace _2026_manajemenruang_backend.Models
{
    public class Ruangan
    {
        public int Id { get; set; }

        [Required]
        public required string NamaRuangan { get; set; }

        [Required]
        public required string Gedung { get; set; }

        [Range(1, 1000)]
        public int Kapasitas { get; set; }
    }
}
