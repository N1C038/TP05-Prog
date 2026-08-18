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

    public IActionResult Index(Usuario usuario)
    {
        if (BD.ExisteUsuario(usuario.nombreUsuario))
        {
            ViewBag.Error = "El nombre de usuario ya existe.";
            return View("Registro");
        }
        else
        {
            BD.RegistrarUsuario(usuario);
            return RedirectToAction("Index");
        }
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
        Usuario usuario = BD.Login(nombreUsuario, contraseña);

        if (usuario != null)
        {
            HttpContext.Session.SetString("nombreUsuario", usuario.nombreUsuario);
            return RedirectToAction("Bienvenida");
        }
        else
        {
            ViewBag.Error = "El usuario o la contraseña son incorrectos.";
            return View("Index");    
        }
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

