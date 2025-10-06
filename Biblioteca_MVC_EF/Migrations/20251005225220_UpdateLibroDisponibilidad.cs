using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Biblioteca.Migrations
{
    /// <inheritdoc />
    public partial class UpdateLibroDisponibilidad : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Disponible",
                table: "libros");

            migrationBuilder.AddColumn<int>(
                name: "EjemplaresDisponibles",
                table: "libros",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EjemplaresDisponibles",
                table: "libros");

            migrationBuilder.AddColumn<bool>(
                name: "Disponible",
                table: "libros",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }
    }
}
