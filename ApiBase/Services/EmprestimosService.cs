using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiBase.Context;
using ApiBase.Services.Interfaces;

namespace ApiBase.Services
{

    public class EmprestimosService : IEmprestimoService
    {
        private readonly EmprestimosContext _context;

        public EmprestimosService(EmprestimosContext context)
        {
            _context = context;
        }

        public void VerificarEAtualizarAtrasos()
        {
            var emprestimosAtrasados = _context.Emprestimo.Where(e => e.DtDevolucao < DateTime.Now && !e.Atrasado).ToList();
            if (emprestimosAtrasados.Count == 0)
                return;

            foreach (var emprestimo in emprestimosAtrasados)
            {
                emprestimo.Atrasado = true;
            }

            _context.SaveChanges();
        }


    }
}
