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

    public async Task<bool> AtualizarAsync(Pessoa pessoa)
    {
        if (!pessoa.EhValido)
            return false;

        _contexto.Pessoas.Update(pessoa);
        await _contexto.SaveChangesAsync();
        return true;
    }

    public async Task<IEnumerable<PessoaRespostaDTO>> ObterPessoasAsync()
    {
        return await _contexto.Pessoas.Select(Mapear()).ToListAsync();
    }

    public async Task<Pessoa?> ObterPorIdAsync(Guid id)
    {
        return await _contexto.Pessoas.AsNoTracking().FirstOrDefaultAsync(e => e.Id == id);
    }

    public async Task<bool> RemoverAsync(Guid id)
    {
        var pessoa = await _contexto.Pessoas.AsNoTracking().FirstOrDefaultAsync(e => e.Id == id);
        if (pessoa == null)
            return false;

        _contexto.Pessoas.Remove(pessoa);
        await _contexto.SaveChangesAsync();
        return true;
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
