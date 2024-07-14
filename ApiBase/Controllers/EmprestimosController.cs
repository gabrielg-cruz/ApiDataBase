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
    public class EmprestimosController : Controller
    {
        private readonly EmprestimosContext _context;

        public EmprestimosController(EmprestimosContext context)
        {
            _context = context;
        }
        [HttpGet("{id}")]
        public IActionResult ObterPorId(int id)
        {
            var Emprestimos = _context.Emprestimo.Find(id);
            if (Emprestimos == null)
                return NotFound();

            return Ok(Emprestimos);
        }
        [HttpGet("ObterTodos")]
        public IActionResult ObterTodos()
        {
            var emprestimos = _context.Emprestimo.ToList();
            return Ok(emprestimos);
        }
        [HttpPost]
        public IActionResult Criar(Emprestimos emprestimo)
        {
            if (emprestimo.DtEmprestimo <= DateTime.Now)
                return BadRequest(new { Erro = "Insira uma data valida" });

            _context.Emprestimo.Add(emprestimo);
            _context.SaveChanges();
            return CreatedAtAction(nameof(ObterPorId), new { Id = emprestimo.Id }, emprestimo);
        }


    }
}