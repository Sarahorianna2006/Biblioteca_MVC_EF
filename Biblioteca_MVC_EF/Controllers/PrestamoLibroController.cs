using Biblioteca.Infrastructure;
using Biblioteca.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Biblioteca.Controllers;

public class PrestamoLibroController : Controller
{
    private readonly AppDbContext  _context;
    
    public PrestamoLibroController(AppDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        var prestamos = _context.prestamoLibros
            .Include(p => p.Usuario)
            .Include(p => p.Libro)
            .ToList();

        ViewBag.Usuarios = _context.usuarios.ToList();
        ViewBag.Libros = _context.libros.Where(l => l.EjemplaresDisponibles > 0).ToList();
        
        return View(prestamos);
    }

    [HttpPost]
    public IActionResult CrearPrestamo(int UsuarioId, int LibroId)
    {
        var libro = _context.libros.FirstOrDefault(l => l.Id == LibroId);

        if (libro == null || libro.EjemplaresDisponibles <= 0)
            return BadRequest("El libro no está disponible");

        var prestamo = new PrestamoLibro
        {
            UsuarioId = UsuarioId,
            LibroId = libro.Id,
            FechaPrestamo = DateTime.Now
        };

        libro.EjemplaresDisponibles--; // el -- resta un ejemplar
        _context.prestamoLibros.Add(prestamo);
        _context.SaveChanges();
        return RedirectToAction("Index");
    }

    public IActionResult DevolverLibro(int id)
    {
        var prestamo = _context.prestamoLibros
            .Include(p => p.Libro)
            .FirstOrDefault(p => p.Id == id);

        if (prestamo == null)
            return NotFound();
        
        prestamo.Devuelto = true;
        prestamo.FechaDevolucion = DateTime.Now;
        prestamo.Libro.EjemplaresDisponibles++; // el ++ devuelve un ejemplar

        _context.SaveChanges();

        return RedirectToAction("Index");
    }

    //Consultar préstamos por usuario
    public IActionResult PrestamosPorUsuario(int usuarioId)
    {
        var prestamos = _context.prestamoLibros
            .Include(p => p.Libro)
            .Include(p => p.Usuario)
            .Where(p => p.UsuarioId == usuarioId)
            .OrderByDescending(p => p.FechaPrestamo)
            .ToList();

        ViewBag.Usuario = _context.usuarios.FirstOrDefault(u => u.Id == usuarioId);

        return View(prestamos);
    }
}
