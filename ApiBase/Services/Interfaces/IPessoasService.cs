using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiBase.Models;
using ApiBase.Models.DTOs;

namespace ApiBase.Services.Interfaces
{
    public interface IPessoasService
    {
        int CalcularIdade(DateTime dtNasc);
        ResultadoDeletarPessoa Deletar(int Id);
        ResultadoAtualizarPessoa Atualizar(int id, Pessoas pessoa);
        ResultadoCriarPessoa Criar(Pessoas pessoa);
        IEnumerable<Pessoas> ObterTodos();
        IEnumerable<Pessoas> ObterPorNome(string nome);
        IEnumerable<Pessoas> ObterPorIdade(int idade);
        IEnumerable<Pessoas> ObterPorEmail(string email);
    }
}