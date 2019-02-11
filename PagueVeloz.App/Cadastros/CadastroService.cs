using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using PagueVeloz.Domain.DTO;
using PagueVeloz.Domain.Entities;
using PagueVeloz.Domain.Interfaces;

namespace PagueVeloz.App.Cadastros
{
    public class CadastroService : ICadastroService
    {
        private readonly IMapper _mapper;
        private readonly IRepository _repository;

        public CadastroService(IMapper mapper, IRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public ICollection<FornecedorDto> ListarFornecedores(FiltroDto filtros)
        {
            var query = _repository.Query<Fornecedor>();

            if (filtros.IdFornecedor > 0)
            {
                var found = query.Where(x => x.Id == filtros.IdFornecedor).ToList();
                return found.Select(x => _mapper.Map<FornecedorDto>(x)).ToList();
            }

            if (!string.IsNullOrWhiteSpace(filtros.Cnpj))
                query = query.Where(x => x.Cnpj == filtros.Cnpj);

            if (!string.IsNullOrWhiteSpace(filtros.Cpf))
                query = query.Where(x => x.Cpf == filtros.Cpf);

            if (!string.IsNullOrWhiteSpace(filtros.Nome))
                query = query.Where(x => x.Nome == filtros.Nome.Trim());

            if (filtros.DataDeCadastroInicial.HasValue)
                query = query.Where(x => x.DataDeCadastro >= filtros.DataDeCadastroInicial);

            if (filtros.DataDeCadastroFinal.HasValue)
                query = query.Where(x => x.DataDeCadastro < filtros.DataDeCadastroFinal.Value.AddDays(1));

            var queryResult = query.ToList();
            return queryResult.Select(x => _mapper.Map<FornecedorDto>(x)).ToList();
        }
        
        public ICollection<EmpresaDto> ListarEmpresas()
        {
            var result = _repository.Query<Empresa>().ToList();
            return result.Select(x => _mapper.Map<EmpresaDto>(x)).ToList();
        }

        public void CadastrarEmpresa(EmpresaDto empresa)
        {
            _repository.SaveOrUpdate(_mapper.Map<Empresa>(empresa));
        }

        public void CadastrarFornecedor(FornecedorDto fornecedor)
        {
            _repository.SaveOrUpdate(_mapper.Map<Fornecedor>(fornecedor));
        }

        public void AlterarFornecedor(long id, FornecedorDto fornecedor)
        {
            var fornecedorExistente = _repository.GetById<Fornecedor>(id);

            fornecedorExistente.Cnpj = fornecedor.Cnpj ?? fornecedorExistente.Cnpj;
            fornecedorExistente.Cpf = fornecedor.Cpf ?? fornecedorExistente.Cpf;
            fornecedorExistente.Rg = fornecedor.Rg ?? fornecedorExistente.Rg;
            fornecedorExistente.Nome = fornecedor.Nome ?? fornecedorExistente.Nome;
            fornecedorExistente.Telefones = fornecedor.Telefones.Any() ? string.Join(';', fornecedor.Telefones) : fornecedorExistente.Telefones;
            fornecedorExistente.DataDeNascimento = fornecedor.DataDeNascimento ?? fornecedorExistente.DataDeNascimento;

            _repository.SaveOrUpdate(fornecedorExistente);
        }

        public void AlterarEmpresa(long id, EmpresaDto empresa)
        {
            var empresaExistente = _mapper.Map<Empresa>(empresa);
            empresaExistente.Id = id;
            _repository.SaveOrUpdate(empresaExistente);
        }

        public void DeletarFornecedor(long id)
        {
            _repository.Delete<Fornecedor>(id);
        }
    }
}
