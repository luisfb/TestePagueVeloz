using System;
using FluentAssertions;
using PagueVeloz.Domain;
using PagueVeloz.Domain.Interfaces;
using PagueVeloz.API;
using PagueVeloz.Domain.Entities;
using Xunit;


namespace PagueVeloz.Tests
{
    public class CadastroEmpresaTest
    {
        private IRepository _repo;

        private Empresa _empresa;

        public CadastroEmpresaTest()
        {
            var teste = Program.BuildWebHost(null);
            _repo = (IRepository)teste.Services.GetService(typeof(IRepository));

        }

        private void DadaUmaEmpresa()
        {
            _empresa = new Empresa("Empresa 1", "SC", "53.076.604/0001-88");
        }

        private void AoCadastrarEmpresa()
        {
            _repo.SaveOrUpdate(_empresa);
        }

        private void DeveTerCadastradoAEmpresaComSucesso()
        {
            _empresa = _repo.GetById<Empresa>(1);
            _empresa.Should().NotBeNull();
            _empresa.Id.Should().Be(1);
            _empresa.NomeFantasia.Should().Be("Empresa 1");
            _empresa.Uf.Should().Be("SC");
            _empresa.Cnpj.Should().Be("53076604000188");
        }

        [Fact]
        public void DadaUmaEmpresa_AoCadastrarEmpresa_DeveTerCadastradoAEmpresaComSucesso()
        {
            DadaUmaEmpresa();
            AoCadastrarEmpresa();
            DeveTerCadastradoAEmpresaComSucesso();
        }

        [Fact]
        public void DadaUmaEmpresa_AoTentarInvalidarOCnpj_DeveApresentarErro()
        {
            DadaUmaEmpresa();
            _empresa.Cnpj = "123abc";
            _empresa.Validate();
            _empresa.ValidationErrors[0].Should().Be("O campo CNPJ de Empresa deve ter 14 números.");
        }

        [Fact]
        public void DadaUmaEmpresa_AoTentarInvalidarONomeFantasia_DeveLimitarA80Caracteres()
        {
            DadaUmaEmpresa();
            _empresa.NomeFantasia = "ZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZ11111111111";
            _empresa.Validate();
            _empresa.NomeFantasia.Should().Be("ZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZ");
            _empresa.NomeFantasia.Length.Should().Be(80);
        }

    }
}
