using Desafio.Umbler.Dominio;
using Microsoft.EntityFrameworkCore;

namespace Desafio.Umbler.Infra.Data.Contexto
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options)
        : base(options)
        {

        }
        
        public DbSet<Domain> Domains { get; set; }
    }
}
