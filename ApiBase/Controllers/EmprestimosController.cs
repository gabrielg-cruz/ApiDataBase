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
        private readonly EmprestimosContext _emprestimoContext;
        private readonly PessoasContext _pessoaContext;
        private readonly LivrosContext _livroContext;

        public EmprestimosController(EmprestimosContext emprestimocontext, PessoasContext pessoacontext, LivrosContext livrocontext)
        {
            _emprestimoContext = emprestimocontext;
            _pessoaContext = pessoacontext;
            _livroContext = livrocontext;
        }
        [HttpGet("{id}")]
        public IActionResult ObterPorId(int id)
        {
            var Emprestimos = _emprestimoContext.Emprestimo.Find(id);
            if (Emprestimos == null)
                return NotFound();

            return Ok(Emprestimos);
        }

        [HttpGet("ObterTodos")]
        public IActionResult ObterTodos()
        {
            var emprestimos = _emprestimoContext.Emprestimo.ToList();
            return Ok(emprestimos);
        }

        [HttpGet("ObterAtrasados")]
        public IActionResult ObterAtrasados()
        {
            var emprestimos = _emprestimoContext.Emprestimo.Where(x => x.Atrasado == true).ToList();
            return Ok(emprestimos);
        }

        [HttpGet("ObterEmprestimosPorPessoa")]
        public IActionResult ObterEmprestimosPorPessoa()
        {
            var EstrangeiraID = _pessoaContext.Pessoa.Select(x => x.Id).ToList();
            var emprestimos = _emprestimoContext.Emprestimo.Where(x => EstrangeiraID.Contains(x.PessoaId)).ToList();
            return Ok(emprestimos);
        }
        [HttpPut("AtualizarAtrasados")]
        public IActionResult AtualizarAtrasados(Emprestimos emprestimos)
        {
            var emprestimo = _emprestimoContext.Emprestimo.Select(x => x.DtDevolução).ToList();
            foreach (DateTime i in emprestimo)
            {
                if (i > DateTime.Now)
                    emprestimos.Atrasado = true;
            }
            return Ok();
        }
        [HttpPost]
        public IActionResult Criar(Emprestimos emprestimo)
        {
            if (emprestimo.DtEmprestimo < DateTime.Now)
                return BadRequest(new { Erro = "A data não pode ser anterior a atual" });

            if (emprestimo.DtDevolução <= DateTime.Now)
                return BadRequest(new { Erro = "A data de emprestimo deve ser uma data futura" });

            _emprestimoContext.Emprestimo.Add(emprestimo);
            _emprestimoContext.SaveChanges();
            return CreatedAtAction(nameof(ObterPorId), new { Id = emprestimo.Id }, emprestimo);
        }


    }
}