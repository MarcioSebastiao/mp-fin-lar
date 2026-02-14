using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MpFinLar.API.Dominio.Entidades;

namespace MpFinLar.API.Infra.Dados.Mapeamentos;

/// <summary>
/// Classe responsável pelo mapeamento da entidade Categoria 
/// utilizando o Entity Framework Core.
/// </summary>
public sealed class CategoriaMapeamento : IEntityTypeConfiguration<Categoria>
{
    public void Configure(EntityTypeBuilder<Categoria> builder)
    {
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id).IsRequired();
        builder.Property(e => e.Descricao)
                .HasMaxLength(400);
        builder.Property(e => e.Finalidade)
               .IsRequired()
               .HasConversion<string>();
    }
}