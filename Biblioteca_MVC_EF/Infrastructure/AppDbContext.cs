using Biblioteca.Models;
using Microsoft.EntityFrameworkCore;

namespace Biblioteca.Infrastructure;

public class AppDbContext : DbContext
{
    public DbSet<Usuario> usuarios { get; set; }
    public DbSet<Libro> libros { get; set; }
    public DbSet<PrestamoLibro> prestamoLibros { get; set; }
    
    DbSet<Historial> historiales { get; set; }


    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}

}

