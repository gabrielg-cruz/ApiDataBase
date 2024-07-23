using Microsoft.AspNetCore.Mvc;
using ApiBase.Context;
using ApiBase.Models;
using System.Reflection.Metadata.Ecma335;
using Microsoft.EntityFrameworkCore;
using ApiBase.Services;
using ApiBase.Services.Interfaces;

namespace ApiBase.Controllers
{

    [Route("[controller]")]
    public class PessoasController : ControllerBase
    {
        private readonly PessoasContext _context;
        private readonly IPessoasService _service;

        public PessoasController(PessoasContext context, IPessoasService service)
        {
            _service = service;
            _context = context;
        }

        [HttpGet("{id}")]
        public IActionResult ObterPorId(int id)
        {
            var pessoa = _context.Pessoa.Find(id);
            if (pessoa == null)
                return NotFound();

            return Ok(pessoa);
        }

        [HttpGet("ObterTodos")]
        public IActionResult ObterTodos()
        {
            var pessoa = _service.ObterTodos();
            if (!pessoa.Any())
                return BadRequest(new { Erro = "Não há registro deste tipo" });
            return Ok(pessoa);
        }

        [HttpGet("ObterPorNome")]
        public IActionResult ObterPorNome(string nome)
        {
            var pessoa = _service.ObterPorNome(nome);
            if (pessoa == null)
                return BadRequest(new { Erro = "Não há registro com este nome" });
            return Ok(pessoa);
        }

        [HttpGet("ObterPorIdadeApartirDe")]
        public IActionResult ObterPorIdade(int idade)
        {
            var pessoaDataNasc = _service.ObterPorIdade(idade);

            if (!pessoaDataNasc.Any())
                return NotFound();

            return Ok(pessoaDataNasc);
        }

        [HttpGet("ObterPorEmail")]
        public IActionResult ObterPorEmail(string email)
        {
            var pessoa = _service.ObterPorEmail(email);
            if (pessoa == null)
                return BadRequest(new { Erro = "Não há registro com este email" });
            return Ok(pessoa);
        }


        [HttpPost]
        public IActionResult Criar(Pessoas pessoa)
        {
            var pessoaCriada = _service.Criar(pessoa);
            if (!pessoaCriada.Sucesso)
                return BadRequest(new { pessoaCriada.Erro });
            return CreatedAtAction(nameof(ObterPorId), new { pessoaCriada.Pessoa.Id }, pessoaCriada.Pessoa);
        }

        [HttpPut("{id}")]
        public IActionResult Atualizar(int id, Pessoas pessoa)
        {
            var PessoaBanco = _service.Atualizar(id, pessoa);
            if (!PessoaBanco.Sucesso)
                return BadRequest(new { PessoaBanco.Erro });
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Deletar(int id)
        {
            var PessoaBanco = _service.Deletar(id);
            return PessoaBanco.Sucesso ? Ok() : BadRequest(new { PessoaBanco.Erro });
        }
    }
}
