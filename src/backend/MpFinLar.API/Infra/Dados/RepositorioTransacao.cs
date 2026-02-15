using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using MpFinLar.API.Aplicacao.Categorias;
using MpFinLar.API.Aplicacao.Transacoes;
using MpFinLar.API.Dominio.Entidades;
using MpFinLar.API.Dominio.Interfaces;

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
        };
    }
}
