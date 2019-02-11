using System;
using FluentAssertions;
using PagueVeloz.API;
using PagueVeloz.Domain;
using PagueVeloz.Domain.Entities;
using PagueVeloz.Domain.Interfaces;
using Xunit;

namespace PagueVeloz.Tests
{
    public class CadastroFornecedorTest
    {
        private IRepository _repo;

        private Empresa _empresa;
        private Fornecedor _fornecedor;

        public CadastroFornecedorTest()
        {
            var teste = Program.BuildWebHost(null);
            _repo = (IRepository)teste.Services.GetService(typeof(IRepository));

        }

        private void DadaUmaEmpresa()
        {
            _empresa = new Empresa("Empresa 1", "PR", "53.076.604/0001-88");
        }

        private void DadoUmFornecedorPJ()
        {
            DadaUmaEmpresa();
            _fornecedor = new Fornecedor("58.044.100/0001-01", "Empresa 1");
            _fornecedor.Empresa = _empresa;
        }

        private void DadoUmFornecedorPF()
        {
            DadaUmaEmpresa();
            _fornecedor = new Fornecedor("010.053.639-51", "3742512", new DateTime(1988, 01, 01), "Luis");
            _fornecedor.Empresa = _empresa;
        }

        private void DadoUmFornecedorPFMenorDeIdade()
        {
            DadaUmaEmpresa();
            _fornecedor = new Fornecedor("010.053.639-51", "3742512", new DateTime(2010, 01, 01), "João");
            _fornecedor.Empresa = _empresa;
        }

        private void DadoUmFornecedorPFSemRgEDataDeNascimento()
        {
            DadaUmaEmpresa();
            _fornecedor = new Fornecedor("010.053.639-51", "3742512", new DateTime(2010, 01, 01), "João");
            _fornecedor.Rg = "";
            _fornecedor.DataDeNascimento = null;
            _fornecedor.Empresa = _empresa;
        }

        private void AoCadastrarFornecedor()
        {
            _repo.SaveOrUpdate(_fornecedor);
        }
        
        private void DeveTerCadastradoAEmpresaComSucesso()
        {
            _empresa = _repo.GetById<Empresa>(1);
            _empresa.Should().NotBeNull();
            _empresa.Id.Should().Be(1);
            _empresa.NomeFantasia.Should().Be("Empresa 1");
            _empresa.Uf.Should().Be("PR");
            _empresa.Cnpj.Should().Be("53076604000188");
        }

        private void DeveTerCadastradoOFornecedorPJComSucesso()
        {
            _fornecedor = _repo.GetById<Fornecedor>(1);
            _fornecedor.Should().NotBeNull();
            _fornecedor.Id.Should().Be(1);
            _fornecedor.IdEmpresa.Should().Be(1);

            _fornecedor.Cpf.Should().BeNull();
            _fornecedor.Rg.Should().BeNull();
            _fornecedor.DataDeNascimento.Should().BeNull();
            _fornecedor.PessoaFisica.Should().BeFalse();
        }

        private void DeveTerCadastradoOFornecedorPFComSucesso()
        {
            _fornecedor = _repo.GetById<Fornecedor>(1);
            _fornecedor.Should().NotBeNull();
            _fornecedor.Id.Should().Be(1);
            _fornecedor.IdEmpresa.Should().Be(1);

            _fornecedor.Cpf.Should().NotBeNullOrEmpty();
            _fornecedor.Rg.Should().NotBeNullOrEmpty();
            _fornecedor.DataDeNascimento.Should().NotBeNull();
            _fornecedor.PessoaFisica.Should().BeTrue();
        }

        [Fact]
        public void DadoUmFornecedorPJ_AoCadastrarFornecedor_DeveTerCadastradoAEmpresaComSucesso_DeveTerCadastradoOFornecedorComSucesso()
        {
            DadoUmFornecedorPJ();
            AoCadastrarFornecedor();
            DeveTerCadastradoAEmpresaComSucesso();
            DeveTerCadastradoOFornecedorPJComSucesso();
        }

        [Fact]
        public void DadoUmFornecedorPF_AoCadastrarFornecedor_DeveTerCadastradoAEmpresaComSucesso_DeveTerCadastradoOFornecedorComSucesso()
        {
            DadoUmFornecedorPF();
            AoCadastrarFornecedor();
            DeveTerCadastradoAEmpresaComSucesso();
            DeveTerCadastradoOFornecedorPFComSucesso();
        }

        [Fact]
        public void DadoUmFornecedorPFMenorDeIdade_AoCadastrarFornecedorNumaEmpresaDoParana_DeveImpedirOCadastro()
        {
            DadoUmFornecedorPFMenorDeIdade();
            EntityValidationException ex = Assert.Throws<EntityValidationException>(() => AoCadastrarFornecedor());
            ex.Erros[0].Should().Be("Fornecedores do Paraná devem ser maiores de idade.");
            ex.Message.Should().Be("Entidade inválida");
        }

        [Fact]
        public void DadoUmFornecedorPFSemRgEDataDeNascimento_AoCadastrarFornecedor_DeveImpedirOCadastro()
        {
            DadoUmFornecedorPFSemRgEDataDeNascimento();
            EntityValidationException ex = Assert.Throws<EntityValidationException>(() => AoCadastrarFornecedor());
            ex.Erros[0].Should().Be("O campo RG de fornecedor deve deve ter entre 5 e 11 números.");
            ex.Erros[1].Should().Be("A data de nascimento é obrigatória para pessoa física.");
            ex.Message.Should().Be("Entidade inválida");
        }
    }
}
