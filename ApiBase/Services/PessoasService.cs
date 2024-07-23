using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiBase.Context;
using ApiBase.Models;
using ApiBase.Models.DTOs;
using ApiBase.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;

namespace ApiBase.Services
{

    public class PessoasService : IPessoasService
    {
        private readonly PessoasContext _PessoasContext;
        private readonly LivrosContext _LivroContext;
        public PessoasService(PessoasContext pessoasContext, LivrosContext livrosContext)
        {
            _PessoasContext = pessoasContext;
            _LivroContext = livrosContext;
        }
        public ResultadoDeletarPessoa Deletar(int Id)
        {
            var PessoaBanco = _PessoasContext.Pessoa.Find(Id);

            if (PessoaBanco == null)
                return new ResultadoDeletarPessoa { Sucesso = false, Erro = "Pessoa não encontrada" };

            _PessoasContext.Pessoa.Remove(PessoaBanco);
            _PessoasContext.SaveChanges();
            return new ResultadoDeletarPessoa { Sucesso = true };
        }
        public ResultadoAtualizarPessoa Atualizar(int id, Pessoas pessoa)
        {
            var PessoaBanco = _PessoasContext.Pessoa.Find(id);

            if (PessoaBanco == null)
                return new ResultadoAtualizarPessoa { Sucesso = false, Erro = "Pessoa não encontrada" };

            if (string.IsNullOrWhiteSpace(pessoa.Nome) || string.IsNullOrWhiteSpace(pessoa.Email))
                return new ResultadoAtualizarPessoa { Sucesso = false, Erro = "Nome ou Email não podem ser vazios" };
            PessoaBanco.Nome = pessoa.Nome;
            PessoaBanco.Email = pessoa.Email;
            PessoaBanco.DtNasc = pessoa.DtNasc;
            _PessoasContext.Pessoa.Update(PessoaBanco);
            _PessoasContext.SaveChanges();
            return new ResultadoAtualizarPessoa { Sucesso = true };

        }
        public ResultadoCriarPessoa Criar(Pessoas pessoa)
        {
            if (string.IsNullOrWhiteSpace(pessoa.Nome) || pessoa.Nome.Length < 2)
                return new ResultadoCriarPessoa { Erro = "Digite um nome válido" };

            if (string.IsNullOrWhiteSpace(pessoa.Email) || pessoa.Email.Length < 3)
                return new ResultadoCriarPessoa { Erro = "Digite um Email válido" };

            pessoa.Atrasos = 0;
            _PessoasContext.Pessoa.Add(pessoa);
            _PessoasContext.SaveChanges();

            return new ResultadoCriarPessoa { Pessoa = pessoa };
        }

        public IEnumerable<Pessoas> ObterPorEmail(string email)
        {
            var pessoa = _PessoasContext.Pessoa.Where(x => x.Email.ToUpper().Contains(email.ToUpper())).ToList();
            return pessoa;
        }
        public IEnumerable<Pessoas> ObterPorIdade(int idade)
        {
            var pessoaDataNasc = _PessoasContext.Pessoa.AsEnumerable().Where(x => CalcularIdade(x.DtNasc) >= idade).ToList();
            return pessoaDataNasc;
        }
        public IEnumerable<Pessoas> ObterPorNome(string nome)
        {
            var pessoa = _PessoasContext.Pessoa.Where(x => x.Nome.ToUpper().Contains(nome.ToUpper())).ToList();
            return pessoa;
        }
        public IEnumerable<Pessoas> ObterTodos()
        {
            return _PessoasContext.Pessoa.ToList();
        }

        public int CalcularIdade(DateTime dtNasc)
        {
            int PessoaIdade = DateTime.Now.Year - dtNasc.Year;

            if (DateTime.Now < dtNasc.AddYears(PessoaIdade))
            {
                PessoaIdade--;
            }

            return PessoaIdade;
        }
    }
}