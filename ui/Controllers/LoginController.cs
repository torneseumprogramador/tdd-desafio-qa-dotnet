using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using tdd_desafio_qa_dotnet.Models;
using tdd_desafio_qa_dotnet.Repositorios;
using tdd_desafio_qa_dotnet.Repositorios.Interfaces;

namespace tdd_desafio_qa_dotnet.Controllers;

public class LoginController : Controller
{
    private IAdministradorRepo _repo;

    public LoginController(IAdministradorRepo admRepo)
    {
        _repo = admRepo;
    }

    public IActionResult Index()
    {
        return View();
    }
}
