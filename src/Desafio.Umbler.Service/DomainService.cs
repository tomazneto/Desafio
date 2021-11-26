using Desafio.Umbler.Dominio;
using Desafio.Umbler.Repository.Interfaces;
using Desafio.Umbler.Service.Interfaces;
using DnsClient;

namespace Desafio.Umbler.Service
{
    public class DomainService : IDomainService
    {
        private readonly IDomainRepository _domainRepository;
        private readonly IWhoisClientService _whoisClientService;
        public DomainService(IDomainRepository domainRepository,
            IWhoisClientService whoisClientService)
        {
            _domainRepository = domainRepository;
            _whoisClientService = whoisClientService;
        }

        public async Task<Domain?> BuscarDominioPorNome(string domainName)
        {
            return await _domainRepository.BuscarDominioPorNome(domainName);
        }

        public async Task<Domain> InserirDominio(Domain domain)
        {
            if(string.IsNullOrEmpty(domain.Name))
                throw new Exception("Dominio é obrigatório!");

            await _domainRepository.Inserir(domain);
            return domain;
        }

        public async Task<Domain> ObterDominio(string domainName)
        {
            var domain = await _domainRepository.BuscarDominioPorNome(domainName);
            return await GerarDominio(domainName, domain);
        }

        private async Task<Domain> GerarDominio(string domainName, Domain? domain)
        {
            if (domain == null)
            {
                domain = new Domain();
                await SetarValores(domain, domainName);
                await _domainRepository.Inserir(domain);
            }

            if (domain.IsActive)
                await SetarValores(domain, domainName);
            else
                throw new Exception("Ocorreu um erro inexperado. Favor contactar o adm do sistema.");

            return domain;
        }

        private async Task SetarValores(Domain domain, string domainName)
        {
            var response = await _whoisClientService.ObterWhois(domainName);
            var lookup = new LookupClient();
            var result = await lookup.QueryAsync(domainName, QueryType.ANY);
            var record = result.Answers.ARecords().FirstOrDefault();
            var address = record?.Address;
            var ip = address?.ToString();
            var hostResponse = await _whoisClientService.ObterWhois(ip);

            domain.Name = domainName;
            domain.Ip = ip;
            domain.UpdatedAt = DateTime.Now;
            domain.WhoIs = response.Raw;
            domain.Ttl = record?.TimeToLive ?? 0;
            domain.HostedAt = string.IsNullOrEmpty(ip) ? response.OrganizationName : hostResponse.OrganizationName;
        }
    }
}
