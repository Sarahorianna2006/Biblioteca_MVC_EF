using Biblioteca.Infrastructure;
using Biblioteca.Models;
using Microsoft.AspNetCore.Mvc;

namespace Biblioteca.Controllers;

public class UsuarioController : Controller
{
    private readonly AppDbContext _context;

    public UsuarioController(AppDbContext context)
    {
        _context = context;
    }
    
    public IActionResult Index()
    {
        var usuarios = _context.usuarios.ToList();
        return View(usuarios);
    }
    
    //Crear usuario (desde el formulario principal)
    [HttpPost]
    public IActionResult Store (Usuario usuario)
    {
        //valida que 'ModelState' sea valido (ej: campos vacios) antes de revisar duplicado de documento
        if (!ModelState.IsValid)
        {
            return View("Index", _context.usuarios.ToList());
        }
        
        //Compreba si el documento no existe ya con un usuario (ya q documento debe ser unico x usuario)
        if (_context.usuarios.Any(u => u.Documento == usuario.Documento))
        {
            ModelState.AddModelError("Documento", "Ya existe un usuario con este documento.");
            return View("Index", _context.usuarios.ToList());
        }
        
        if (ModelState.IsValid)
        {
            _context.usuarios.Add(usuario);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        //Si hay errores, recargam la vista con los datos actuales
        return View("Index", _context.usuarios.ToList());
    }
    
    [HttpGet]
    public IActionResult Edit(int id)
    {
        var usuarios = _context.usuarios.FirstOrDefault(u => u.Id == id);
        if (usuarios == null) 
            return NotFound();
        
        return View(usuarios);
    }

    [HttpPost]
    public IActionResult Edit(Usuario usuario)
    {
        var usuarioOriginal = _context.usuarios.FirstOrDefault(u => u.Id == usuario.Id);
        if (usuarioOriginal == null) 
            return NotFound();
        
        usuarioOriginal.Nombre = usuario.Nombre;
        usuarioOriginal.Documento = usuario.Documento;
        usuarioOriginal.Email = usuario.Email;
        usuarioOriginal.Telefono = usuario.Telefono;

        _context.SaveChanges();
        
        return RedirectToAction("Index");
    }
    
    public IActionResult Delete(int id)
    {
        var usuarios = _context.usuarios.FirstOrDefault(u => u.Id == id);
        if (usuarios == null) 
            return NotFound();
        
        _context.usuarios.Remove(usuarios);
        _context.SaveChanges();
        return RedirectToAction("Index");
    }
}