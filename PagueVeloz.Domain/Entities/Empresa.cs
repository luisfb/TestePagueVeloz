using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using PagueVeloz.Domain.Entities;

namespace PagueVeloz.Domain.Entities
{
    public class Empresa : EntityBase
    {
        public string NomeFantasia { get; set; }
        public string Uf { get; set; }
        public string Cnpj { get; set; }
        public virtual ICollection<Fornecedor> Fornecedores { get; } = new List<Fornecedor>();

        public Empresa() { }

        public Empresa(string nomeFantasia, string uf, string cnpj)
        {
            NomeFantasia = nomeFantasia;
            Uf = uf;
            Cnpj = cnpj;
        }

        public override bool Validate()
        {
            var regex = new Regex(@"\D");
            Cnpj = regex.Replace(Cnpj ?? string.Empty, string.Empty);

            if (Cnpj.Length != 14)
                ValidationErrors.Add("O campo CNPJ de Empresa deve ter 14 números.");

            if (string.IsNullOrWhiteSpace(Uf) || Uf.Length != 2)
                ValidationErrors.Add("O campo UF de Empresa deve conter dois caracteres.");

            Uf = Uf?.ToUpper();

            if (string.IsNullOrWhiteSpace(NomeFantasia))
                ValidationErrors.Add("O nome da empresa é obrigatório.");

            if (NomeFantasia?.Length > 80)
                NomeFantasia = NomeFantasia?.Substring(0, 80);

            return ValidationErrors.Count == 0;
        }
    }
}
