using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ApiBase.Context;
using ApiBase.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ApiBase.Services;
using ApiBase.Services.Interfaces;

namespace ApiBase.Controllers
{
    [Route("[controller]")]

    public class EmprestimosController : Controller
    {
        private readonly IEmprestimoService _emprestimoService;
        private readonly EmprestimosContext _emprestimoContext;
        private readonly PessoasContext _pessoaContext;
        private readonly LivrosContext _livroContext;

        public EmprestimosController(EmprestimosContext emprestimocontext, PessoasContext pessoacontext, LivrosContext livrocontext, IEmprestimoService emprestimoservice)
        {
            _emprestimoService = emprestimoservice;
            _emprestimoContext = emprestimocontext;
            _pessoaContext = pessoacontext;
            _livroContext = livrocontext;
        }

        [HttpGet("{id}")]
        public IActionResult ObterPorId(int id)
        {
            _emprestimoService.VerificarEAtualizarAtrasos();
            var Emprestimos = _emprestimoContext.Emprestimo.Find(id);
            if (Emprestimos == null)
                return NotFound();
            return Ok(Emprestimos);
        }

        [HttpGet("ObterTodos")]
        public IActionResult ObterTodos()
        {
            _emprestimoService.VerificarEAtualizarAtrasos();
            var emprestimos = _emprestimoContext.Emprestimo.ToList();
            return Ok(emprestimos);
        }

        [HttpGet("ObterAtrasados")]
        public IActionResult ObterAtrasados()
        {
            _emprestimoService.VerificarEAtualizarAtrasos();
            var emprestimos = _emprestimoContext.Emprestimo.Where(x => x.Atrasado == true).ToList();

            if (emprestimos.Count == 0)
                return NoContent();
            return Ok(emprestimos);
        }

        [HttpGet("ObterEmprestimosPorPessoa")]
        public IActionResult ObterEmprestimosPorPessoa(int id)
        {
            _emprestimoService.VerificarEAtualizarAtrasos();
            var EstrangeiraID = _pessoaContext.Pessoa.Select(x => x.Id).ToList();
            var emprestimos = _emprestimoContext.Emprestimo.Where(x => EstrangeiraID.Contains(x.PessoaId)).ToList();
            return Ok(emprestimos);
        }

        [HttpPost]
        public IActionResult Criar(Emprestimos emprestimo)
        {
            DateTime Hoje = DateTime.Now;
            DateTime DataFormatada = new(Hoje.Year, Hoje.Month, Hoje.Day);
            var livro = _livroContext.Livro.SingleOrDefault(x => x.Id == emprestimo.LivroId);

            if (emprestimo.DtDevolucao <= DateTime.Now)
                return BadRequest(new { Erro = "A data de emprestimo deve ser uma data futura" });

            if (livro == null)
                return BadRequest(new { Erro = "Livro não existe" });

            if (livro.Estado == LivroEstado.Reservado)
                return BadRequest(new { Erro = "Livro esta reservado" });

            if (livro.Estado == LivroEstado.Emprestado)
            {
                livro.Estado = LivroEstado.Reservado;
                emprestimo.Atrasado = false;
                _emprestimoContext.Emprestimo.Add(emprestimo);
                _emprestimoContext.SaveChanges();
                _livroContext.Livro.Update(livro);
                _livroContext.SaveChanges();
                return CreatedAtAction(nameof(ObterPorId), new { Id = emprestimo.Id }, emprestimo);
            }
            emprestimo.DtEmprestimo = DataFormatada;
            emprestimo.Atrasado = false;
            _emprestimoContext.Emprestimo.Add(emprestimo);
            _emprestimoContext.SaveChanges();
            livro.Estado = LivroEstado.Emprestado;
            _livroContext.Livro.Update(livro);
            _livroContext.SaveChanges();
            return CreatedAtAction(nameof(ObterPorId), new { Id = emprestimo.Id }, emprestimo);
        }

        [HttpDelete("DeletarEmprestimo")]
        public IActionResult DeletarEmprestimo(int id)
        {
            _emprestimoService.VerificarEAtualizarAtrasos();
            var EmprestimoBanco = _emprestimoContext.Emprestimo.Find(id);
            if (EmprestimoBanco == null)
                return NotFound();
            if (EmprestimoBanco.Atrasado == true)
            {
                var pessoa = _pessoaContext.Pessoa.Find(EmprestimoBanco.PessoaId);
                if (pessoa != null)
                {
                    pessoa.Atrasos += 1;
                    _pessoaContext.Pessoa.Update(pessoa);
                    _pessoaContext.SaveChanges();
                }
            }
            _emprestimoContext.Emprestimo.Remove(EmprestimoBanco);
            _emprestimoContext.SaveChanges();
            return NoContent();

        }
    }
}