using Microsoft.AspNetCore.Authorization
using Microsoft.AspNetCore.Mvc;
using ExoApi.Contexts;
using ExoApi.Models;
using System;
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

        // GET: api/projetos
        [HttpGet]
        public IActionResult Listar()
        {
            try
            {
                var lista = _context.Projetos.ToList();
                return Ok(lista);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { 
                    erro = "Ocorreu um erro interno ao processar a solicitacao.", 
                    detalhes = ex.Message 
                });
            }
        }

        // GET: api/projetos/{id}
        [HttpGet("{id}")]
        public IActionResult BuscarPorId(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest(new { erro = "O ID informado deve ser um numero maior que zero." });
                }

                var proyecto = _context.Projetos.Find(id);
                
                if (proyecto == null)
                {
                    return NotFound(new { erro = $"Projeto com o ID {id} nao foi encontrado." });
                }

                return Ok(proyecto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { erro = "Erro ao buscar o projeto.", detalhes = ex.Message });
            }
        }

        // POST: api/projetos
        [HttpPost]
        public IActionResult Cadastrar(Projeto projeto)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(projeto.Nome))
                {
                    return BadRequest(new { erro = "O nome do projeto e obrigatorio e nao pode estar vazio." });
                }

                if (string.IsNullOrWhiteSpace(projeto.Area))
                {
                    return BadRequest(new { erro = "A area do projeto e obrigatoria." });
                }

                _context.Projetos.Add(projeto);
                _context.SaveChanges();

                return StatusCode(201, new { mensagem = "Projeto cadastrado com sucesso!", dados = projeto });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { erro = "Falha ao salvar o novo projeto.", detalhes = ex.Message });
            }
        }

        // PUT: api/projetos/{id}
        [HttpPut("{id}")]
        public IActionResult Atualizar(int id, Projeto projeto)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest(new { erro = "ID fornecido e invalido." });
                }

                var projetoExistente = _context.Projetos.Find(id);
                if (projetoExistente == null)
                {
                    return NotFound(new { erro = $"Impossivel atualizar. Projeto com ID {id} nao existe." });
                }

                if (string.IsNullOrWhiteSpace(projeto.Nome) || string.IsNullOrWhiteSpace(projeto.Area))
                {
                    return BadRequest(new { erro = "Os campos Nome e Area precisam estar preenchidos para atualizacao." });
                }

                projetoExistente.Nome = projeto.Nome;
                projetoExistente.Area = projeto.Area;
                projetoExistente.Status = projeto.Status;

                _context.Projetos.Update(projetoExistente);
                _context.SaveChanges();

                return Ok(new { mensagem = "Projeto atualizado com sucesso!", dados = projetoExistente });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { erro = "Erro interno ao atualizar o projeto.", detalhes = ex.Message });
            }
        }

        // DELETE: api/projetos/{id}
        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult Deletar(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest(new { erro = "ID informado e invalido para remocao." });
                }

                var projeto = _context.Projetos.Find(id);
                if (projeto == null)
                {
                    return NotFound(new { erro = $"Impossivel deletar. Projeto com ID {id} nao foi localizado." });
                }

                _context.Projetos.Remove(projeto);
                _context.SaveChanges();

                return Ok(new { mensagem = $"Projeto com ID {id} removido com sucesso de forma permanente." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { erro = "Erro critico ao deletar o registro no banco de dados.", detalhes = ex.Message });
            }
        }
    }
}
