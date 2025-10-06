using Biblioteca.Infrastructure;
using Biblioteca.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Biblioteca.Controllers
{
    public class LibroController : Controller
    {
        private readonly AppDbContext _context;
    
        public LibroController(AppDbContext context)
        {
            _context = context;
        }

        //Mostrar lista de libros
        public IActionResult Index()
        {
            var libros = _context.libros.ToList();
            return View(libros);
        }
    
        //Crear libro (desde el formulario principal)
        [HttpPost]
        public IActionResult CrearLibro(Libro libro)
        {
            //Validar código único
            if (_context.libros.Any(l => l.Codigo == libro.Codigo))
            {
                ModelState.AddModelError("Codigo", "Ya existe un libro con este código.");
                return View("Index", _context.libros.ToList());
            }

            if (ModelState.IsValid)
            {
                _context.libros.Add(libro);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            //Si hay errores, recarga la vista con los datos actuales
            return View("Index", _context.libros.ToList());
        }

        //Editar libro
        [HttpGet]
        public IActionResult EditarLibro(int id)
        {
            var libro = _context.libros.FirstOrDefault(l => l.Id == id);
            if (libro == null)
                return NotFound();

            return View(libro);
        }

        // Editar libro
        [HttpPost]
        public IActionResult EditarLibro(Libro libro)
        {
            var libroOriginal = _context.libros.FirstOrDefault(l => l.Id == libro.Id);
            if (libroOriginal == null)
                return NotFound();

            // Actualizamos los campos editables
            libroOriginal.Codigo = libro.Codigo;
            libroOriginal.Titulo = libro.Titulo;
            libroOriginal.Autor = libro.Autor;
            libroOriginal.EjemplaresDisponibles = libro.EjemplaresDisponibles;

            _context.SaveChanges();
        
            return RedirectToAction("Index");
        }

        //Eliminar libro
        public IActionResult EliminarLibro(int id)
        {
            var libro = _context.libros.FirstOrDefault(l => l.Id == id);
            if (libro == null)
                return NotFound();

            _context.libros.Remove(libro);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
