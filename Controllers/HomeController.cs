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
        if (BD.ExisteUsuario(usuario.NombreUsuario) == true)
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
        string NombreUsuario = HttpContext.Session.GetString("nombreUsuario");
        if (NombreUsuario == null)
        {
            return RedirectToAction("Index");
        }
        Usuario usuario = BD.ObtenerUsuario(NombreUsuario);
        if (usuario == null)
        {
            return RedirectToAction("Index");
        }
        ViewBag.Usuario = usuario.NombreUsuario;
        ViewBag.Nombre = usuario.Nombre;
        ViewBag.Apellido = usuario.Apellido;
        ViewBag.TipoUsuario = usuario.TipoUsuario;
        ViewBag.IDEspecialidad = usuario.IDEspecialidad;
        ViewBag.Especialidad = BD.ObtenerEspecialidad(usuario.IDEspecialidad).Nombre;
        return View();
    }
//Hecha con Copilot
    public IActionResult Login(Usuario usuario)
    {
        Usuario usuarioAutenticado = BD.IniciarSesion(usuario.NombreUsuario, usuario.Contraseña);

        if (usuarioAutenticado != null)
        {
            HttpContext.Session.SetString("nombreUsuario", usuarioAutenticado.NombreUsuario);
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

