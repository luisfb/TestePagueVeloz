using System.Linq;
using AutoMapper;
using PagueVeloz.Domain.DTO;
using PagueVeloz.Domain.Entities;

namespace PagueVeloz.App.MappingProfiles
{
    public class DomainToDto : Profile
    {
        public DomainToDto()
        {
            CreateMap<Empresa, EmpresaDto>();
            CreateMap<Fornecedor, FornecedorDto>()
                .ForMember(x => x.Telefones, y => y.MapFrom(z => z.ObterTelefones().ToList()));
        }
    }
}
