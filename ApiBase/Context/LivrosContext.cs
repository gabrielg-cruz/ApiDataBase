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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Livros>()
                .Property(e => e.Estado)
                .HasColumnType("tinyint")
                .HasConversion(
                    v => (int)v,
                    v => (LivroEstado)v);

            base.OnModelCreating(modelBuilder);
        }
    }
}