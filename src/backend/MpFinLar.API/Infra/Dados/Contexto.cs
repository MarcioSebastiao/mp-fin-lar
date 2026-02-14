using Microsoft.EntityFrameworkCore;
using MpFinLar.API.Dominio.Entidades;

namespace MpFinLar.API.Infra.Dados;

/// <summary>
/// Classe que representa o contexto do banco de dados da aplicação.
/// Responsável por gerenciar as entidades e a conexão com o banco
/// </summary>
public sealed class Contexto : DbContext
{
    /// <summary>
    /// Conjunto de entidades Pessoa que será mapeado para a tabela correspondente no banco de dados.
    /// </summary>
    public DbSet<Pessoa> Pessoas { get; set; }

    /// <summary>
    /// Conjunto de entidades Categoria que será mapeado para a tabela correspondente no banco de dados.
    /// </summary>
    public DbSet<Categoria> Categorias { get; set; }

    /// <summary>
    /// Conjunto de entidades Transacao que será mapeado para a tabela correspondente no banco de dados.
    /// </summary>
    public DbSet<Transacao> Transacoes { get; set; }

    public Contexto(DbContextOptions<Contexto> options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(Contexto).Assembly);
    }
}
