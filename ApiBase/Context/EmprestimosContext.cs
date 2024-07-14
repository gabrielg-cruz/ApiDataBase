using Microsoft.EntityFrameworkCore;
using ApiBase.Models;

namespace ApiBase.Context
{
    public class EmprestimosContext : DbContext
    {
        public EmprestimosContext(DbContextOptions<EmprestimosContext> options) : base(options)
        {

        }

        public DbSet<Emprestimos> Emprestimo { get; set; }
    }
}