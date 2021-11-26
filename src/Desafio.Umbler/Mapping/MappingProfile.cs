using AutoMapper;
using Desafio.Umbler.Dominio;
using Desafio.Umbler.Dominio.Model;

namespace Desafio.Umbler.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Domain, DomainModel>();
        }
    }
}
