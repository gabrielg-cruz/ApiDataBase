using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiBase.Context;
using ApiBase.Models;
using ApiBase.Services.Interfaces;

namespace ApiBase.Services
{

    public class EmprestimosService : IEmprestimoService
    {
        private readonly EmprestimosContext _EmprestimoContext;
        private readonly LivrosContext _LivroContext;

        public EmprestimosService(EmprestimosContext emprestimoContext, LivrosContext livrosContext)
        {
            _EmprestimoContext = emprestimoContext;
            _LivroContext = livrosContext;
        }

        public void VerificarEAtualizarAtrasos()
        {
            var emprestimosAtrasados = _EmprestimoContext.Emprestimo.Where(x => x.DtDevolucao < DateTime.Now && !x.Atrasado).ToList();

            if (emprestimosAtrasados.Count == 0)
                return;

            foreach (var emprestimo in emprestimosAtrasados)
            {
                emprestimo.Atrasado = true;
            }

            _EmprestimoContext.SaveChanges();
        }
    }
}