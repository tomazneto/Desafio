using Desafio.Umbler.Dominio;

namespace Desafio.Umbler.Repository.Interfaces
{
    public interface IDomainRepository
    {
        Task<Domain?> BuscarDominioPorNome(string domainName);
        Task<int> Inserir(Domain domain);
    }
}
