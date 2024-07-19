using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiBase.Context;
using ApiBase.Models;
using ApiBase.Services.Interfaces;

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