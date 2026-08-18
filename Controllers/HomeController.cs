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
