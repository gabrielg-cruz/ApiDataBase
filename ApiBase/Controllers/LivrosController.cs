using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ApiBase.Context;
using ApiBase.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ApiBase.Controllers
{
    [Route("[controller]")]
    public class LivrosController : Controller
    {
        private readonly LivrosContext _context;

        public LivrosController(LivrosContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        public IActionResult ObterPorId(int id)
        {
            var Livros = _context.Livro.Find(id);
            if (Livros == null)
                return NotFound();

            return Ok(Livros);
        }

        [HttpGet("ObterTodos")]
        public IActionResult ObterTodos()
        {
            var livro = _context.Livro.ToList();
            return Ok(livro);
        }
        [HttpGet("ObterEmprestados")]
        public IActionResult ObterEmprestados()
        {
            var livro = _context.Livro.Where(x => x.Estado == LivroEstado.Emprestado).ToList();
            return Ok(livro);
        }
        [HttpGet("ObterDisponiveis")]
        public IActionResult ObterDisponiveis()
        {
            var livro = _context.Livro.Where(x => x.Estado == LivroEstado.Livre).ToList();
            return Ok(livro);
        }
        [HttpPost]
        public IActionResult Criar(Livros livro)
        {
            if (livro.Titulo.Trim() == null || livro.Titulo.Trim() == "")
                return BadRequest(new { Erro = "Campo Titulo é obrigatório" });
            if (livro.Titulo.Length < 2)
                return BadRequest(new { Erro = "Digite um Titulo valido" });


            if (livro.Editora.Trim() == "" || livro.Editora.Trim() == null)
                return BadRequest(new { Erro = "Campo Editora é obrigatório" });
            if (livro.Editora.Length < 3)
                return BadRequest(new { Erro = "Digite um Editora valido" });

            livro.Estado = LivroEstado.Livre;
            _context.Livro.Add(livro);
            _context.SaveChanges();
            return CreatedAtAction(nameof(ObterPorId), new { Id = livro.Id }, livro);
        }
        [HttpDelete("{id}")]
        public IActionResult Deletar(int id)
        {
            var LivrosBanco = _context.Livro.Find(id);

            if (LivrosBanco == null)
                return NotFound();

            _context.Livro.Remove(LivrosBanco);
            _context.SaveChanges();
            return NoContent();
        }
    }
}