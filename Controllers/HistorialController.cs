using Biblioteca.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Biblioteca.Controllers;

public class HistorialController : Controller
{
    private readonly AppDbContext _context;

    public HistorialController(AppDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        var historial  = _context.prestamoLibros
            .Include(p => p.Usuario)
            .Include(p => p.Libro)
            .OrderByDescending(p => p.FechaPrestamo)
            .ToList();
        return View(historial);
    }
}
