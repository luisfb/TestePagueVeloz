using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace PagueVeloz.Domain.Entities
{
    public class Fornecedor : EntityBase
    {
        public virtual Empresa Empresa { get; set; }
        public long IdEmpresa { get; set; }
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public string Rg { get; set; }
        public string Cnpj { get; set; }
        public string Telefones { get; set; }
        public bool PessoaFisica => string.IsNullOrEmpty(Cnpj);
        public DateTime? DataDeNascimento { get; set; }
        public DateTime DataDeCadastro { get; set; } = DateTime.Now;

        public Fornecedor(string cpf, string rg, DateTime nascimento, string nome)
        {
            Nome = nome;
            Cpf = cpf;
            Rg = rg;
            DataDeNascimento = nascimento;
        }

        public Fornecedor(string cnpj, string nome)
        {
            Nome = nome;
            Cnpj = cnpj;
        }

        public IEnumerable<string> ObterTelefones()
        {
            return Telefones?.Split(';', StringSplitOptions.RemoveEmptyEntries);
        }

        public void AdicionarTelefone(string telefone)
        {
            Telefones = string.Concat(Telefones ?? string.Empty, ";", telefone);
        }

        public override bool Validate()
        {
            var regex = new Regex(@"\D");

            if (PessoaFisica)
            {
                Cpf = regex.Replace(Cpf ?? string.Empty, string.Empty);
                Rg = regex.Replace(Rg ?? string.Empty, string.Empty);

                if (Cpf.Length != 11)
                    ValidationErrors.Add("O campo CPF de fornecedor deve ter 11 números.");

                if (Rg.Length > 11 || Rg.Length < 5)
                    ValidationErrors.Add("O campo RG de fornecedor deve deve ter entre 5 e 11 números.");

                if (!DataDeNascimento.HasValue)
                    ValidationErrors.Add("A data de nascimento é obrigatória para pessoa física.");
            }
            else
            {
                Cnpj = regex.Replace(Cnpj ?? string.Empty, string.Empty);
                if (Cnpj.Length != 14)
                    ValidationErrors.Add("O campo CNPJ de fornecedor deve ter 14 números.");
            }

            if (Empresa == null)
            {
                ValidationErrors.Add("O fornecedor precisa ter uma empresa vinculada para realizar o cadastro");
                return false;
            }

            if (Empresa.Uf == "PR")
            {
                if (PessoaFisica && DataDeNascimento.HasValue)
                {
                    var idade = (DateTime.Now - DataDeNascimento.Value).Days / 365;
                    if(idade < 18)
                        ValidationErrors.Add("Fornecedores do Paraná devem ser maiores de idade.");
                }
            }

            if (!Empresa.Validate())
                foreach (string erro in Empresa.ValidationErrors)
                    ValidationErrors.Add(erro);
            
            return ValidationErrors.Count == 0;
        }
    }
}
