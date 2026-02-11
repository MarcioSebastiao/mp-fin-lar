using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MpFinLar.API.Dominio.Entidades;

namespace MpFinLar.API.Infra.Dados.Mapeamentos;

/// <summary>
/// Classe responsável pelo mapeamento da entidade Pessoa
/// utilizando o Entity Framework Core.
/// </summary>
public sealed class PessoaMapeamento : IEntityTypeConfiguration<Pessoa>
{
    public void Configure(EntityTypeBuilder<Pessoa> builder)
    {
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id).IsRequired();
        builder.Property(e => e.Nome)
                .HasMaxLength(200);
    }
}
