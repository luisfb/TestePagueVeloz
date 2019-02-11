using System;
using System.Collections.Generic;
using System.Text;
using PagueVeloz.Domain.DTO;

namespace PagueVeloz.Domain.Interfaces
{
    public interface ICadastroService
    {
        ICollection<FornecedorDto> ListarFornecedores(FiltroDto filtros);
        ICollection<EmpresaDto> ListarEmpresas();
        void CadastrarEmpresa(EmpresaDto empresa);
        void CadastrarFornecedor(FornecedorDto fornecedor);
        void AlterarFornecedor(long id, FornecedorDto fornecedor);
        void AlterarEmpresa(long id, EmpresaDto empresa);
        void DeletarFornecedor(long id);
    }
}
