using System.Linq;
using AutoMapper;
using PagueVeloz.Domain.DTO;
using PagueVeloz.Domain.Entities;

namespace PagueVeloz.App.MappingProfiles
{
    public class DtoToDomain : Profile
    {
        public DtoToDomain()
        {
            CreateMap<EmpresaDto, Empresa>()
                .ForMember(x => x.Id, y => y.Ignore());
            CreateMap<FornecedorDto, Fornecedor>()
                .ForMember(x => x.Telefones, y => y.MapFrom(z => string.Join(';', z.Telefones)))
                .ForMember(x => x.Id, y => y.Ignore());
        }
    }
}
