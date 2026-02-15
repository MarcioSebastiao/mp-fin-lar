using MpFinLar.API.Aplicacao.Categorias;
using MpFinLar.API.Aplicacao.Pessoas;
using MpFinLar.API.Dominio.Entidades;
using MpFinLar.API.Dominio.Interfaces;

namespace MpFinLar.API.Aplicacao.Transacoes;

public sealed class TransacaoAplicacao : ITransacaoAplicacao
{
    private readonly IRepositorioTransacao _repositorio;
    private readonly IRepositorioPessoa _repositorioPessoa;
    private readonly IRepositorioCategoria _repositorioCategoria;

    public TransacaoAplicacao(IRepositorioTransacao repositorio, IRepositorioPessoa repositorioPessoa, IRepositorioCategoria repositorioCategoria)
    {
        _repositorio = repositorio;
        _repositorioPessoa = repositorioPessoa;
        _repositorioCategoria = repositorioCategoria;
    }

    public async Task<(TransacaoRespostaDTO?, ResultadoAplicacao)> CriarAsync(TransacaoDto transacaoDto)
    {
        var pessoa = await _repositorioPessoa.ObterPorIdAsync(transacaoDto.PessoaId);
        if (pessoa is null)
            return new(null, new(["Pessoa não encontrada."]));

        var categoria = await _repositorioCategoria.ObterPorIdAsync(transacaoDto.CategoriaId);
        if (categoria is null)
            return new(null, new(["Categoria não encontrada."]));

        var transacao = new Transacao(transacaoDto.Descricao, transacaoDto.Valor, transacaoDto.Tipo, categoria, pessoa);

        if (!transacao.EhValido)
            return new(null, new(transacao.Notificacoes));

        await _repositorio.AdicionarAsync(transacao);

        return new(MapearParaResposta(transacao, categoria, pessoa), new(Array.Empty<string>()));
    }

    public Task<IEnumerable<TransacaoRespostaDTO>> ObterTransacoesDePessoaAsync(Guid pessoaId, int pularItens = 0, int quantidadeItens = 100)
    {
        if(quantidadeItens <= 0 || quantidadeItens > 100)
            quantidadeItens = 100;
        return _repositorio.ObterTransacoesDePessoaAsync(pessoaId, pularItens, quantidadeItens);
    }

    private static TransacaoRespostaDTO MapearParaResposta(Transacao transacao, Categoria categoria, Pessoa pessoa) => new()
    {
        Id = transacao.Id,
        Descricao = transacao.Descricao,
        Valor = transacao.Valor,
        Tipo = transacao.Tipo.ToString(),
        Categoria = new CategoriaRespostaDTO
        {
            Id = categoria.Id,
            Descricao = categoria.Descricao,
            Finalidade = categoria.Finalidade.ToString()
    
        },
        Pessoa = new PessoaRespostaDTO
        {
            Id = pessoa.Id,
            Nome = pessoa.Nome,
            Idade = pessoa.Idade
        }
    };
}
