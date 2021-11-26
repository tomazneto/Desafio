using Whois.NET;

namespace Desafio.Umbler.Service.Interfaces
{
    public interface IWhoisClientService
    {
        Task<WhoisResponse> ObterWhois(string query);
    }
}
