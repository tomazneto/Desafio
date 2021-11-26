using Desafio.Umbler.Dominio;
using Desafio.Umbler.Infra.Data.Contexto;
using Desafio.Umbler.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Desafio.Umbler.Repository.Repositories
{
    public class DomainRepository : IDomainRepository
    {
        private readonly DatabaseContext _db;

        public DomainRepository(DatabaseContext db)
        {
            _db = db;
        }

        public async Task<Domain?> BuscarDominioPorNome(string domainName)
        {
            return await _db.Domains.FirstOrDefaultAsync(d => d.Name == domainName);
        }

        public async Task<int> Inserir(Domain domain)
        {
            _db.Domains.Add(domain);
            return await _db.SaveChangesAsync();
        }
    }
}
