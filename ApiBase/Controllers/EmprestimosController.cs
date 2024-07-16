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
            var Emprestimos = _emprestimoContext.Emprestimos.Find(id);
            if (Emprestimos == null)
                return NotFound();

            return Ok(Emprestimos);
        }

        [HttpGet("ObterTodos")]
        public IActionResult ObterTodos()
        {
            var emprestimos = _emprestimoContext.Emprestimos.ToList();
            return Ok(emprestimos);
        }

        [HttpGet("ObterAtrasados")]
        public IActionResult ObterAtrasados()
        {
            var emprestimos = _emprestimoContext.Emprestimos.Where(x => x.Atrasado == true).ToList();
            return Ok(emprestimos);
        }

        [HttpGet("ObterEmprestimosPorPessoa")]
        public IActionResult ObterEmprestimosPorPessoa(int id)
        {
            var EstrangeiraID = _pessoaContext.Pessoa.Select(x => x.Id).ToList();
            var emprestimos = _emprestimoContext.Emprestimos.Where(x => EstrangeiraID.Contains(x.PessoaId)).ToList();
            return Ok(emprestimos);
        }

        //          <<TO DO>>
        //[HttpPut("AtualizarAtrasados")]
        /*public IActionResult AtualizarAtrasados(Emprestimos emprestimos)
        {
            var emprestimo = _emprestimoContext.Emprestimos.Select(x => x.DtDevolucao).ToList();
            foreach (DateTime i in emprestimo)
            {
                if (i > DateTime.Now)
                    emprestimos.Atrasado = true;
            }
            return Ok(emprestimo);
        }*/

        [HttpPost]
        public IActionResult Criar(Emprestimos emprestimo)
        {
            /*if (emprestimo.DtEmprestimo < DateTime.Now)
                return BadRequest(new { Erro = $"A data não pode ser anterior a atual{DateTime.Now}" });*/

            if (emprestimo.DtDevolucao <= DateTime.Now)
                return BadRequest(new { Erro = "A data de emprestimo deve ser uma data futura" });
            var livro = _livroContext.Livros.Where(x => x.Id == emprestimo.LivroId);
            _emprestimoContext.Emprestimos.Add(emprestimo);
            _emprestimoContext.SaveChanges();

            if (livro == null)
                return BadRequest(new { Erro = "Livro não existe" });
            return CreatedAtAction(nameof(ObterPorId), new { Id = emprestimo.Id }, emprestimo);

        }


    }
}