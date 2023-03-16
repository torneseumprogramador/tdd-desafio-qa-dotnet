using Microsoft.AspNetCore.Mvc;
using tdd_desafio_qa_dotnet.Models;
using tdd_desafio_qa_dotnet.Repositorios.Interfaces;

namespace tdd_desafio_qa_dotnet.Controllers.Api
{
    [ApiController]
    [Route("api/administradores")]
    public class AdministradoresApiController : ControllerBase
    {
        private readonly IAdministradorRepo _administradorRepo;

        public AdministradoresApiController(IAdministradorRepo administradorRepo)
        {
            _administradorRepo = administradorRepo;
        }

        // GET: api/Administradores
        [HttpGet]
        public IActionResult Index()
        {
            var administradores = _administradorRepo.BuscarTodos();
            return Ok(administradores);
        }

        [HttpGet("{id}/truncate")]
        public IActionResult Truncate(int id)
        {
            _administradorRepo.Truncate();
            return NoContent();
        }

        // GET: api/Administradores/5
        [HttpGet("{id}")]
        public IActionResult Show(int id)
        {
            var administrador = _administradorRepo.BuscaPorId(id);

            if (administrador == null)
            {
                return NotFound();
            }

            return Ok(administrador);
        }

        // PUT: api/Administradores/5
        [HttpPut("{id}")]
        public IActionResult Update(int id, Administrador administrador)
        {
            if (id != administrador.Id)
            {
                return BadRequest();
            }

            var admExistente = _administradorRepo.BuscaPorId(id);

            if (admExistente == null)
            {
                return NotFound();
            }

            _administradorRepo.Salvar(administrador);

            return Ok(administrador);
        }

        // POST: api/Administradores
        [HttpPost]
        public IActionResult Create(Administrador administrador)
        {
            _administradorRepo.Salvar(administrador);

            return CreatedAtAction(nameof(Show), new { id = administrador.Id }, administrador);
        }

        // DELETE: api/Administradores/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var administrador = _administradorRepo.BuscaPorId(id);

            if (administrador == null)
            {
                return NotFound();
            }

            _administradorRepo.Excluir(administrador);

            return NoContent();
        }

        
    }
}
