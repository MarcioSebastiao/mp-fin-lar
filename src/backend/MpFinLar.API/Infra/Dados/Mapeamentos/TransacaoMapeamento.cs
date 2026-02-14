using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MpFinLar.API.Dominio.Entidades;

namespace MpFinLar.API.Infra.Dados.Mapeamentos;

public sealed class TransacaoMapeamento : IEntityTypeConfiguration<Transacao>
{
    public void Configure(EntityTypeBuilder<Transacao> builder)
    {
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id).IsRequired();

        builder.Property(e => e.Descricao)
               .HasMaxLength(400);

        builder.Property(e => e.Valor)
               .IsRequired()
               .HasColumnType("decimal(18,2)");

        builder.Property(e => e.Tipo)
               .IsRequired()
               .HasConversion<string>();

        // Relacionamento com Categoria
        builder.HasOne(e => e.Categoria)
               .WithMany() 
               .HasForeignKey(e => e.CategoriaId)
               .OnDelete(DeleteBehavior.Restrict); // Evita exclusão em cascata 

        // Relacionamento com Pessoa
        builder.HasOne(e => e.Pessoa)
               .WithMany(p => p.Transacoes)
               .HasForeignKey(e => e.PessoaId)
               .OnDelete(DeleteBehavior.Cascade); // Exclui transações quando a pessoa for excluída
    }
}