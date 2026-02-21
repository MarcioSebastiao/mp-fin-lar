using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using MpFinLar.API.Aplicacao.Categorias;
using MpFinLar.API.Dominio.Entidades;
using MpFinLar.API.Dominio.Interfaces;

namespace MpFinLar.API.Infra.Dados;

public sealed class RepositorioCategoria : IRepositorioCategoria
{
    private readonly Contexto _contexto;

    public RepositorioCategoria(Contexto contexto)
    {
        _contexto = contexto;
    }

    public async Task AdicionarAsync(Categoria categoria)
    {

        if (!categoria.EhValido)
            return;

        _contexto.Categorias.Add(categoria);
        await _contexto.SaveChangesAsync();

    }

    public async Task<IEnumerable<CategoriaRespostaDTO>> ObterCategoriasAsync(int pularItens, int quantidadeItens)
    {
        return await _contexto.Categorias
        .AsNoTracking()
        .OrderByDescending(c => c.DataCriacao)
        .Skip(pularItens)
        .Take(quantidadeItens)
        .Select(Mapear()).ToListAsync();
    }

    public async Task<bool> CategoriaTemTransacoes(Guid id)
    {
        return await _contexto.Transacoes.AsNoTracking().AnyAsync(p => p.CategoriaId == id);
    }

    public async Task RemoverAsync(Guid id)
    {
        _contexto.Categorias.Where(c => c.Id == id).ExecuteDelete();
        await _contexto.SaveChangesAsync();
    }

    /// <summary>
    /// Expressão Linq para mapear de entidade Categoria para a DTO
    /// </summary>
    /// <returns></returns>
    private static Expression<Func<Categoria, CategoriaRespostaDTO>> Mapear()
    {
        return categoria => new CategoriaRespostaDTO
        {
            Id = categoria.Id,
            DataCriacao = categoria.DataCriacao,
            Descricao = categoria.Descricao,
            Finalidade = categoria.Finalidade.ToString()
        };
    }

    public Task<int> ObterTotalDeItensAsync() => _contexto.Categorias.CountAsync();

    public Task<Categoria?> ObterPorIdAsync(Guid id)
    {
        return _contexto.Categorias.FirstOrDefaultAsync(c => c.Id == id);
    }
}