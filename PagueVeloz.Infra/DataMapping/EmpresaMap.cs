using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PagueVeloz.Domain.Entities;


namespace PagueVeloz.Infra.DataMapping
{
    public class EmpresaMap : IEntityTypeConfiguration<Empresa>
    {
        public void Configure(EntityTypeBuilder<Empresa> builder)
        {
            builder.Ignore(x => x.ValidationErrors);

            builder
                .HasKey(x => x.Id);

            builder
                .Property(x => x.Id)
                .ValueGeneratedOnAdd();

            builder
                .Property(x => x.Cnpj)
                .HasColumnName("cnpj")
                .IsRequired();

            builder
                .Property(x => x.NomeFantasia)
                .HasColumnName("nome_fantasia")
                .IsRequired();

            builder
                .Property(x => x.Uf)
                .HasColumnName("uf")
                .IsRequired();

            builder
                .HasMany(x => x.Fornecedores)
                .WithOne(x => x.Empresa)
                .HasForeignKey(x => x.IdEmpresa);

        }
    }
}
