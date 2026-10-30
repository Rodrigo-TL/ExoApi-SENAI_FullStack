using Microsoft.AspNetCore.Mvc;
using ExoApi.Contexts;
using ExoApi.Models;
using System.Linq;

namespace ExoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjetosController : ControllerBase
    {
        private readonly ExoContext _context;

        public ProjetosController()
        {
            _context = new ExoContext();
        }

        [HttpGet]
        public IActionResult Listar()
        {
            return Ok(_context.Projetos.ToList());
        }

        [HttpGet("{id}")]
        public IActionResult BuscarPorId(int id)
        {
            var projeto = _context.Projetos.Find(id);
            if (projeto == null) return NotFound(new { mensagem = "Projeto nao localizado." });
            return Ok(projeto);
        }

        [HttpPost]
        public IActionResult Cadastrar(Projeto projeto)
        {
            _context.Projetos.Add(projeto);
            _context.SaveChanges();
            return StatusCode(201, new { mensagem = "Projeto cadastrado com sucesso!" });
        }

        [HttpPut("{id}")]
        public IActionResult Atualizar(int id, Projeto projeto)
        {
            var projetoExistente = _context.Projetos.Find(id);
            if (projetoExistente == null) return NotFound(new { mensagem = "Projeto nao localizado." });

            projetoExistente.Nome = projeto.Nome;
            projetoExistente.Area = projeto.Area;
            projetoExistente.Status = projeto.Status;

            _context.Projetos.Update(projetoExistente);
            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Deletar(int id)
        {
            var projeto = _context.Projetos.Find(id);
            if (projeto == null) return NotFound(new { mensagem = "Projeto nao localizado." });

            _context.Projetos.Remove(projeto);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
