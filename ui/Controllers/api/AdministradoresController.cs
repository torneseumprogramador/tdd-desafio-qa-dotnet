using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using tdd_desafio_qa_dotnet.Models;
using tdd_desafio_qa_dotnet.Contexto;

namespace tdd_desafio_qa_dotnet.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdministradoresController : ControllerBase
    {
        private readonly DbContexto _context;

        public AdministradoresController(DbContexto context)
        {
            _context = context;
        }

        // GET: api/Administradores
        [HttpGet]
        public IActionResult Index()
        {
            var administradores = _context.Administradores.ToList();
            return Ok(administradores);
        }

        // GET: api/Administradores/5
        [HttpGet("{id}")]
        public IActionResult Show(int id)
        {
            var administrador = _context.Administradores.Find(id);

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

            _context.Entry(administrador).State = EntityState.Modified;

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AdministradorExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(administrador);
        }

        // POST: api/Administradores
        [HttpPost]
        public IActionResult Create(Administrador administrador)
        {
            _context.Administradores.Add(administrador);
            _context.SaveChanges();

            return CreatedAtAction(nameof(Show), new { id = administrador.Id }, administrador);
        }

        // DELETE: api/Administradores/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var administrador = _context.Administradores.Find(id);
            if (administrador == null)
            {
                return NotFound();
            }

            _context.Administradores.Remove(administrador);
            _context.SaveChanges();

            return NoContent();
        }

        private bool AdministradorExists(int id)
        {
            return _context.Administradores.Any(e => e.Id == id);
        }
    }
}
