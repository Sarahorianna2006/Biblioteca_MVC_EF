using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Biblioteca.Migrations
{
    /// <inheritdoc />
    public partial class AddDevueltoColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Fecha",
                table: "prestamoLibros");

            migrationBuilder.AddColumn<bool>(
                name: "Devuelto",
                table: "prestamoLibros",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaDevolucion",
                table: "prestamoLibros",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaPrestamo",
                table: "prestamoLibros",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Devuelto",
                table: "prestamoLibros");

            migrationBuilder.DropColumn(
                name: "FechaDevolucion",
                table: "prestamoLibros");

            migrationBuilder.DropColumn(
                name: "FechaPrestamo",
                table: "prestamoLibros");

            migrationBuilder.AddColumn<DateOnly>(
                name: "Fecha",
                table: "prestamoLibros",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));
        }
    }
}
