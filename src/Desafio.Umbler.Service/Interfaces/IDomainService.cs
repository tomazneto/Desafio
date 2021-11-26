using Desafio.Umbler.Dominio;

namespace Desafio.Umbler.Service.Interfaces
{
    public interface IDomainService
    {
        Task<Domain> ObterDominio(string domainName);
        Task<Domain> InserirDominio(Domain domain);
        Task<Domain?> BuscarDominioPorNome(string domainName);
    }
}
