using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiBase.Models;

namespace ApiBase.Services.Interfaces
{
    public interface IPessoasService
    {
        int CalcularIdade(DateTime dtNasc);
        IEnumerable<Pessoas> ObterTodos();
        IEnumerable<Pessoas> ObterPorNome(string nome);
        IEnumerable<Pessoas> ObterPorIdade(int idade);
        IEnumerable<Pessoas> ObterPorEmail(string email);
    }
}