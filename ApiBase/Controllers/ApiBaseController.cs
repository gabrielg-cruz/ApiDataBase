using Microsoft.AspNetCore.Mvc;
using ApiBase.Context;
using ApiBase.Models;

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
            var pessoa = _context.Pessoa.Where(x => x.Nome.ToUpper().Contains(nome.ToUpper())).ToList();
            return Ok(pessoa);
        }

        /*    [HttpGet("ObterPorIdade")]
            public IActionResult ObterPorIdade(DateTime dataNascimento)
            {
                DateTime pessoaDataNasc = _context.Pessoa.Where(x => x.DataNascimento.Date == dataNascimento.Date);
                var idade = pessoaDataNasc.Year - DateTime.Now.Year;

                if (DateTime.Now < pessoaDataNasc.AddYears(idade))
                {
                    idade--;
                }
                return Ok(idade);
            }*/

        [HttpGet("ObterPorEmail")]
        public IActionResult ObterPorEmail(string email)
        {
            var pessoa = _context.Pessoa.Where(x => x.Email.ToUpper().Contains(email.ToUpper())).ToList();
            return Ok(pessoa);
        }

        [HttpPost]
        public IActionResult Criar(Pessoas pessoa)
        {
            if (pessoa.Nome == null && pessoa.Email == null)
                return BadRequest(new { Erro = "A data da tarefa não pode ser vazia" });

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

            if (PessoaBanco.Nome == null && PessoaBanco.Email == null)
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
