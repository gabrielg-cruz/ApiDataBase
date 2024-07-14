using Microsoft.EntityFrameworkCore;
using ApiBase.Models;

namespace ApiBase.Context
{
    public class LivrosContext : DbContext
    {
        public LivrosContext(DbContextOptions<LivrosContext> options) : base(options)
        {

        }

        public DbSet<Livros> Livro { get; set; }
    }
}