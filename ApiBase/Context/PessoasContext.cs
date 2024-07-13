using Microsoft.EntityFrameworkCore;
using ApiBase.Models;

namespace ApiBase.Context
{
    public class PessoasContext : DbContext
    {
        public PessoasContext(DbContextOptions<PessoasContext> options) : base(options)
        {

        }

        public DbSet<Pessoas> Pessoa { get; set; }
    }
}