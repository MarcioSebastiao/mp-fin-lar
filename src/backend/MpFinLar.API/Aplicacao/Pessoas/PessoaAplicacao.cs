using MpFinLar.API.Dominio.Entidades;
using MpFinLar.API.Dominio.Interfaces;

namespace MpFinLar.API.Aplicacao.Pessoas;

public sealed class PessoaAplicacao : IPessoaAplicacao
{
    private readonly IRepositorioPessoa _repositorio;

    public PessoaAplicacao(IRepositorioPessoa repositorio)
    {
        _repositorio = repositorio;
    }

    public async Task<ResultadoAplicacao> CriarAsync(PessoaDTO dto)
    {
        var pessoa = new Pessoa(dto.Nome, dto.Idade);

        if (!pessoa.EhValido)
            return new(pessoa.Notificacoes);

        await _repositorio.AdicionarAsync(pessoa);

        return new(Array.Empty<string>());
    }
}
