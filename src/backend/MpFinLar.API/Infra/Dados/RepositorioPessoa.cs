using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using MpFinLar.API.Aplicacao.Pessoas;
using MpFinLar.API.Dominio.Entidades;
using MpFinLar.API.Dominio.Interfaces;

namespace MpFinLar.API.Infra.Dados;

public sealed class RepositorioPessoa : IRepositorioPessoa
{
    private readonly Contexto _contexto;

    public RepositorioPessoa(Contexto contexto)
    {
        _contexto = contexto;
    }

    public async Task AdicionarAsync(Pessoa pessoa)
    {
        if (!pessoa.EhValido)
            return;

        _contexto.Pessoas.Add(pessoa);
        await _contexto.SaveChangesAsync();
    }

    public async Task<IEnumerable<PessoaRespostaDTO>> ObterPessoasAsync()
    {
        return await _contexto.Pessoas.Select(Mapear()).ToListAsync();
    }

    /// <summary>
    /// Expressão Linq para mapear de entidade Pessoa para a DTO
    /// </summary>
    /// <returns></returns>
    private static Expression<Func<Pessoa, PessoaRespostaDTO>> Mapear()
    {
        return pessoa => new PessoaRespostaDTO
        {
            Id = pessoa.Id,
            Nome = pessoa.Nome,
            Idade = pessoa.Idade
        };
    }

}
