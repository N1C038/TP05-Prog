using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TP5.Models;
/*
2. Login
Cualquier usuario registrado puede iniciar sesión ingresando:

nombre de usuario
contraseña
Condiciones:

validar credenciales contra la base de datos
si son correctas → guardar usuario en Session y redirigir a una acción del controller de bienvenida.
si son incorrectas → mostrar mensaje de error
Antes de enviar el formulario, validar que los campos requeridos hayan sido completados.*/
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
        RedirectToAction("Bienvenida", "Home");
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
