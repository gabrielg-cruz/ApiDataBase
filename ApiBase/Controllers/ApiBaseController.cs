using Microsoft.AspNetCore.Mvc;
using ApiBase.Context;
using ApiBase.Models;
using System.Reflection.Metadata.Ecma335;
using Microsoft.EntityFrameworkCore;

namespace ApiBase.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ApiBaseController : ControllerBase
    {
        private readonly OrganizadorContext _context;

        public ApiBaseController(OrganizadorContext context)
        {
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
            var pessoa = _context.Pessoa.ToList();
            return Ok(pessoa);
        }

        [HttpGet("ObterPorNome")]
        public IActionResult ObterPorNome(string nome)
        {
            var pessoa = _context.Pessoa.Where(x => x.Nome.ToUpper().Contains(nome.ToUpper())).ToListAsync();
            return Ok(pessoa);
        }

        [HttpGet("ObterPorIdadeApartirDe")]
        public IActionResult ObterPorIdade(int idade)
        {
            var pessoaDataNasc = _context.Pessoa.AsEnumerable().Where(x => CalcularIdade(x.DtNasc) >= idade).ToList();

            if (pessoaDataNasc.Count == 0)
                return NotFound();

            return Ok(pessoaDataNasc);
        }
        private int CalcularIdade(DateTime dtNasc)
        {
            int PessoaIdade = DateTime.Now.Year - dtNasc.Year;

            if (DateTime.Now < dtNasc.AddYears(PessoaIdade))
            {
                PessoaIdade--;
            }
            Console.WriteLine(PessoaIdade);
            return PessoaIdade;
        }

        [HttpGet("ObterPorEmail")]
        public IActionResult ObterPorEmail(string email)
        {
            var pessoa = _context.Pessoa.Where(x => x.Email.ToUpper().Contains(email.ToUpper())).ToList();
            return Ok(pessoa);
        }

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

            _context.Pessoa.Add(pessoa);
            _context.SaveChanges();
            return CreatedAtAction(nameof(ObterPorId), new { Id = pessoa.Id }, pessoa);
        }

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
