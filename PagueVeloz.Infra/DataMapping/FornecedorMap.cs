using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PagueVeloz.Domain.Entities;

namespace PagueVeloz.Infra.DataMapping
{
    public class FornecedorMap : IEntityTypeConfiguration<Fornecedor>
    {
        public void Configure(EntityTypeBuilder<Fornecedor> builder)
        {
            builder.Ignore(x => x.ValidationErrors);

            builder
                .HasKey(x => x.Id);

            builder
                .Property(x => x.Id)
                .ValueGeneratedOnAdd();

            builder
                .HasOne(x => x.Empresa)
                .WithMany(x => x.Fornecedores)
                .HasForeignKey(x => x.IdEmpresa);

            builder
                .Property(x => x.IdEmpresa)
                .IsRequired();

            builder
                .Property(x => x.Nome)
                .HasColumnName("nome")
                .IsRequired();

            builder
                .Property(x => x.Cpf)
                .HasColumnName("cpf");

            builder
                .Property(x => x.Cnpj)
                .HasColumnName("cnpj");

            builder
                .Property(x => x.Rg)
                .HasColumnName("rg");

            builder
                .Property(x => x.Telefones)
                .HasColumnName("telefones");

            builder
                .Property(x => x.DataDeCadastro)
                .HasColumnName("data_cadastro")
                .HasDefaultValue(DateTime.Now)
                .IsRequired();

            builder
                .Property(x => x.DataDeNascimento)
                .HasColumnName("data_nascimento");

        }
    }
}
