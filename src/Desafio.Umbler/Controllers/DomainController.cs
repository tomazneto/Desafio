using AutoMapper;
using Desafio.Umbler.Dominio;
using Desafio.Umbler.Dominio.Model;
using Desafio.Umbler.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Desafio.Umbler.Controllers
{
    [Route("api")]
    public class DomainController : Controller
    {
        private readonly IDomainService _service;
        private readonly IMapper _mapper;

        public DomainController(IDomainService service,
            IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet, Route("domain/{domainName}")]
        public async Task<IActionResult> Get(string domainName)
        {
            try
            {
                if(string.IsNullOrEmpty(domainName))
                    throw new Exception("Informe um dominio valido!");

                var domain = await _service.ObterDominio(domainName);
                return Ok(_mapper.Map<DomainModel>(domain));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        public async Task<IActionResult> InserirDominio(Domain domain)
        {
            try
            {
                var  result = await _service.InserirDominio(domain);
                return Ok(_mapper.Map<DomainModel>(result));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        public async Task<IActionResult> GetErrorBadRequest(string domainName)
        {
            try
            {
                if (string.IsNullOrEmpty(domainName))
                    throw new Exception("domainName é obrigatório");

                var result = await _service.ObterDominio(domainName);

                return Ok(domainName);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        public async Task<IActionResult> GetErrorNotFound(string domainName)
        {
            try
            {
                var result = await _service.BuscarDominioPorNome(domainName);

                if (result == null)
                    return NotFound();

                return Ok(domainName);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
