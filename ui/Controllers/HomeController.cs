using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using tdd_desafio_qa_dotnet.Models;
using tdd_desafio_qa_dotnet.Repositorios;
using tdd_desafio_qa_dotnet.Repositorios.Interfaces;

namespace tdd_desafio_qa_dotnet.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private IAdministradorRepo _repo;

    public HomeController(ILogger<HomeController> logger, IAdministradorRepo admRepo)
    {
        _logger = logger;
        _repo = admRepo;
    }

    public IActionResult Index()
    {
        ViewBag.administradores = _repo.BuscarTodos();
        return View();
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
