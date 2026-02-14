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

    public async Task<IEnumerable<CategoriaRespostaDTO>> ObterCategoriasAsync()
    {
        return await _contexto.Categorias.Select(Mapear()).ToListAsync();
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
            Descricao = categoria.Descricao,
            Finalidade = categoria.Finalidade.ToString()
        };
    }
}