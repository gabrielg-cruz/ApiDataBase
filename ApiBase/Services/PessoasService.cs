using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiBase.Context;
using ApiBase.Models;
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