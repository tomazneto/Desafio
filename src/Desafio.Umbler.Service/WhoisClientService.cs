using Desafio.Umbler.Service.Interfaces;
using Whois.NET;

namespace Desafio.Umbler.Service
{
    public class WhoisClientService : IWhoisClientService
    {
        public async Task<WhoisResponse> ObterWhois(string query)
        {
            return await WhoisClient.QueryAsync(query);
        }
    }
}
