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
}
