using Desafio.Umbler.Dominio;
using Desafio.Umbler.Service.Interfaces;
using DnsClient;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using Whois.NET;

namespace Desafio.Umbler.Controllers
{
    public class TestController : Controller
    {
        public async Task<IActionResult> Get(string domainName, WhoisResponse whoisResponse)
        {
            try
            {
                var response = whoisResponse;
                var lookup = new LookupClient();
                var result = await lookup.QueryAsync(domainName, QueryType.ANY);
                var record = result.Answers.ARecords().FirstOrDefault();
                var address = record?.Address;
                var ip = address?.ToString();

                var domain = new Domain
                {
                    Name = domainName,
                    Ip = ip,
                    UpdatedAt = DateTime.Now,
                    WhoIs = response.Raw,
                    Ttl = record?.TimeToLive ?? 0,
                    HostedAt = response.OrganizationName
                };

                return Ok(domain);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
