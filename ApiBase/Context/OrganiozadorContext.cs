using Microsoft.EntityFrameworkCore;
using ApiBase.Models;

namespace ApiBase.Context
{
    public class OrganizadorContext : DbContext
    {
        public OrganizadorContext(DbContextOptions<OrganizadorContext> options) : base(options)
        {

        }

        public DbSet<Pessoas> PessoasBD { get; set; }
    }
}