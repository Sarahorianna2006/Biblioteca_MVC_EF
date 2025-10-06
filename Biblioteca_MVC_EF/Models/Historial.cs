namespace Biblioteca.Models;

public class Historial
{
    public int Id { get; set; }
    public int PrestamoLibroId { get; set; }
    public PrestamoLibro PrestamoLibro {get; set;}
}