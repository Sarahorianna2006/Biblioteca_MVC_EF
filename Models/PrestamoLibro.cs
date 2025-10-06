namespace Biblioteca.Models;

public class PrestamoLibro
{
    public int Id  { get; set; }
    
    public int UsuarioId { get; set; }
    public Usuario Usuario { get; set; }
    
    public int LibroId { get; set; }
    public Libro Libro { get; set; }
    
    public DateTime FechaPrestamo { get; set; }
    public DateTime? FechaDevolucion { get; set; }
    public bool Devuelto { get; set; } = false;
}