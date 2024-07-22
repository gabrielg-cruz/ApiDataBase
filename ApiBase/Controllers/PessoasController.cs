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

        //TODO: Implementar o método ObterPorAtraso
        [HttpPost]
        public IActionResult Criar(Pessoas pessoa)
        {
            if (pessoa.Nome.Trim() == null || pessoa.Nome.Trim() == "")
                return BadRequest(new { Erro = "Campo Nome é obrigatório" });
            if (pessoa.Nome.Length < 2)
                return BadRequest(new { Erro = "Digite um nome valido" });


            if (pessoa.Email.Trim() == "" || pessoa.Email.Trim() == null)
                return BadRequest(new { Erro = "Campo Email é obrigatório" });
            if (pessoa.Email.Length < 3)
                return BadRequest(new { Erro = "Digite um Email valido" });

            pessoa.Atrasos = 0;
            _context.Pessoa.Add(pessoa);
            _context.SaveChanges();
            return CreatedAtAction(nameof(ObterPorId), new { Id = pessoa.Id }, pessoa);
        }
        //TODO: Implementar o método Atualizar
        [HttpPut("{id}")]
        public IActionResult Atualizar(int id, Pessoas pessoa)
        {
            var PessoaBanco = _context.Pessoa.Find(id);

            if (PessoaBanco == null)
                return NotFound();

            if (pessoa.Nome.Trim() == null || pessoa.Nome.Trim() == "" || pessoa.Email.Trim() == "" || pessoa.Email.Trim() == null)
                return BadRequest(new { Erro = "O Nome e o Email são obrigatórios" });
            PessoaBanco.Nome = pessoa.Nome;
            PessoaBanco.Email = pessoa.Email;
            PessoaBanco.DtNasc = pessoa.DtNasc;
            _context.Pessoa.Update(PessoaBanco);
            _context.SaveChanges();
            return Ok();
        }
        //TODO: Implementar o método Deletar
        [HttpDelete("{id}")]
        public IActionResult Deletar(int id)
        {
            var PessoaBanco = _context.Pessoa.Find(id);

            if (PessoaBanco == null)
                return NotFound();

            _context.Pessoa.Remove(PessoaBanco);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
