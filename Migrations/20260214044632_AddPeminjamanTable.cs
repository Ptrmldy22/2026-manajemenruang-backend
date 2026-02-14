using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace _2026_manajemenruang_backend.Migrations
{
    /// <inheritdoc />
    public partial class AddPeminjamanTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Peminjaman",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    RuanganId = table.Column<int>(type: "int", nullable: false),
                    NamaPeminjam = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TanggalPinjam = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    TanggalSelesai = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Keterangan = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Peminjaman", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Peminjaman_Ruangans_RuanganId",
                        column: x => x.RuanganId,
                        principalTable: "Ruangans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Peminjaman_RuanganId",
                table: "Peminjaman",
                column: "RuanganId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Peminjaman");
        }
    }
}
