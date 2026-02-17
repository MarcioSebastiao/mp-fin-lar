using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using MpFinLar.API.Aplicacao.Transacoes;
using MpFinLar.API.Dominio.Entidades;
using MpFinLar.API.Dominio.Enums;
using MpFinLar.API.Dominio.Interfaces;
using MpFinLar.API.Dominio.Modelos;

namespace MpFinLar.API.Infra.Dados;

public sealed class RepositorioTransacao : IRepositorioTransacao
{
    private readonly Contexto _contexto;

    public RepositorioTransacao(Contexto contexto)
    {
        _contexto = contexto;
    }

    public async Task AdicionarAsync(Transacao transacao)
    {
        if (!transacao.EhValido)
            return;

        _contexto.Transacoes.Add(transacao);
        await _contexto.SaveChangesAsync();
    }

    public async Task<IEnumerable<TransacaoRespostaDTO>> ObterTransacoesDePessoaAsync(Guid pessoaId, int pularItens, int quantidadeItens)
    {
        return await _contexto.Transacoes
        .AsNoTracking()
        .Where(t => t.PessoaId == pessoaId)
        .Skip(pularItens)
        .Take(quantidadeItens)
        .Select(Mapear()).ToListAsync();
    }

    public async Task<ValoresTransacoes> ObterValoresDePessoaAsync(Guid pessoaId)
    {
        var valores = await _contexto.Transacoes
        .AsNoTracking()
        .Where(t => t.PessoaId == pessoaId)
        .GroupBy(t => t.Tipo)
        .Select(g => new
        {
            Tipo = g.Key,
            Total = g.Sum(t => t.Valor)
        }).ToListAsync();

        return new ValoresTransacoes(
            totalEmDespesas: valores.FirstOrDefault(v => v.Tipo == TipoTrasacao.Despesa)?.Total ?? 0,
            totalEmReceitas: valores.FirstOrDefault(v => v.Tipo == TipoTrasacao.Receita)?.Total ?? 0
        );
    }

    public async Task<ValoresTransacoes> ObterValoresPorCategoriaAsync(Guid categoriaId)
    {
        var valores = await _contexto.Transacoes
        .AsNoTracking()
        .Where(t => t.CategoriaId == categoriaId)
        .GroupBy(t => t.Tipo)
        .Select(g => new
        {
            Tipo = g.Key,
            Total = g.Sum(t => t.Valor)
        }).ToListAsync();

        return new ValoresTransacoes(
            totalEmDespesas: valores.FirstOrDefault(v => v.Tipo == TipoTrasacao.Despesa)?.Total ?? 0,
            totalEmReceitas: valores.FirstOrDefault(v => v.Tipo == TipoTrasacao.Receita)?.Total ?? 0
        );
    }

    public Task<int> ObterTotalDeTransacoesDePessoaAsync(Guid pessoaId)
    {
        return _contexto.Transacoes
        .AsNoTracking()
        .Where(t => t.PessoaId == pessoaId)
        .CountAsync();
    }

    /// <summary>
    /// Expressão Linq para mapear de entidade Transacao para a DTO
    /// </summary>
    /// <returns></returns>
    private static Expression<Func<Transacao, TransacaoRespostaDTO>> Mapear()
    {
        return transacao => new TransacaoRespostaDTO
        {
            Id = transacao.Id,
            Descricao = transacao.Descricao,
            Valor = transacao.Valor,
            Tipo = transacao.Tipo.ToString(),
            Categoria = new()
            {
                Descricao = transacao.Categoria.Descricao
            }
        };
    }
}
