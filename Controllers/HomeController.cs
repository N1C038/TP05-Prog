using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TP5.Models;

namespace TP5.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        //validar credenciales contra la base de datos
        Usuario usuario = new Usuario();
        
        return View();
    }

    public IActionResult Bienvenida()
   {
        string nombreUsuario = HttpContext.Session.GetString("nombreUsuario");
        if (nombreUsuario == null)
        {
            return RedirectToAction("Index");
        }
        Usuario usuario = BD.ObtenerUsuario(nombreUsuario);
        if (usuario == null)
        {
            return RedirectToAction("Index");
        }
        ViewBag.Usuario = Usuario.NombreUsuario;
        ViewBag.Nombre = Usuario.Nombre;
        ViewBag.Apellido = Usuario.Apellido;
        ViewBag.TipoUsuario = Usuario.TipoUsuario;
        ViewBag.IDEspecialidad = Usuario.IDEspecialidad;
        ViewBag.Especialidad = BD.ObtenerEspecialidad(usuario.IDEspecialidad).Nombre;
        return View();
    }

    public IActionResult Login()
    {
        RedirectToAction("Login", "Home");
        return View();
    }

    public IActionResult CerrarSesion()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Index", "Home");
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
