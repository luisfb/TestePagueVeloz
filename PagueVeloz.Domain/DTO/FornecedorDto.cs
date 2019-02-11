using System;
using System.Collections.Generic;
using PagueVeloz.Domain.Entities;

namespace PagueVeloz.Domain.DTO
{
    public class FornecedorDto
    {
        public long Id { get; set; }
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public string Rg { get; set; }
        public string Cnpj { get; set; }
        public IList<string> Telefones { get; set; } = new List<string>();
        public bool PessoaFisica { get; set; }
        public DateTime? DataDeNascimento { get; set; }
        public DateTime? DataDeCadastro { get; set; }
        public EmpresaDto Empresa { get; set; }

    }
}
