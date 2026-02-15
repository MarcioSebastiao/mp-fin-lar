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

    public async Task<(PessoaRespostaDTO?, ResultadoAplicacao)> CriarAsync(PessoaDTO dto)
    {
        var pessoa = new Pessoa(dto.Nome, dto.Idade);

        if (!pessoa.EhValido)
            return new(null, new(pessoa.Notificacoes));

        await _repositorio.AdicionarAsync(pessoa);

        return new(MapearParaResposta(pessoa), new(Array.Empty<string>()));
    }

    public async Task<(PessoaRespostaDTO?, ResultadoAplicacao)> AtualizarAsync(Guid id, PessoaDTO dto)
    {
        var pessoa = await _repositorio.ObterPorIdAsync(id);
        if (pessoa is null)
            return new(null, new(["Pessoa não encontrada."]));

        pessoa.Atualizar(dto.Nome, dto.Idade);

        var sucesso = await _repositorio.AtualizarAsync(pessoa);
        if (!sucesso)
            return new(null, new(pessoa.Notificacoes));

        return new(MapearParaResposta(pessoa), new(Array.Empty<string>()));
    }

    public async Task<ResultadoAplicacao> RemoverAsync(Guid id)
    {
        var sucesso = await _repositorio.RemoverAsync(id);
        if (!sucesso)
            return new(["Pessoa não encontrada."]);

        return new(Array.Empty<string>());
    }

    public async Task<PessoasRespostaDTO> ObterAsync(int pularItens = 0, int quantidadeItens = 100)
    {
        if (quantidadeItens <= 0 || quantidadeItens > 100)
            quantidadeItens = 100;

        var pessoas = await _repositorio.ObterPessoasAsync(pularItens, quantidadeItens);
        var totalItens = await _repositorio.ObterTotalDeItensAsync();

        return new PessoasRespostaDTO
        {
            Pessoas = pessoas,
            TotalDeItens = totalItens
        };
    }

    private PessoaRespostaDTO? MapearParaResposta(Pessoa pessoa) => new()
    {
        Id = pessoa.Id,
        Nome = pessoa.Nome,
        Idade = pessoa.Idade
    };

}
