using MpFinLar.API.Dominio.Entidades;
using MpFinLar.API.Dominio.Interfaces;

namespace MpFinLar.API.Aplicacao.Categorias;

public sealed class CategoriaAplicacao : ICategoriaAplicacao
{
    private readonly IRepositorioCategoria _repositorio;

    public CategoriaAplicacao(IRepositorioCategoria repositorio)
    {
        _repositorio = repositorio;
    }

    public async Task<(CategoriaRespostaDTO?, ResultadoAplicacao)> CriarAsync(CategoriaDto dto)
    {
        var categoria = new Categoria(dto.Descricao, dto.Finalidade);

        if (!categoria.EhValido)
            return new(null, new(categoria.Notificacoes));

        await _repositorio.AdicionarAsync(categoria);

        return new(MapearParaResposta(categoria), new(Array.Empty<string>()));
    }

    public async Task<CategoriasRespostaDTO> ObterAsync(int pularItens = 0, int quantidadeItens = 100)
    {
        if (quantidadeItens <= 0 || quantidadeItens > 100)
            quantidadeItens = 100;

        var categorias = await _repositorio.ObterCategoriasAsync(pularItens, quantidadeItens);
        var totalItens = await _repositorio.ObterTotalDeItensAsync();

        return new CategoriasRespostaDTO
        {
            Categorias = categorias,
            TotalDeItens = totalItens
        };
    }

    public async Task<ResultadoAplicacao> RemoverAsync(Guid id)
    {
        if (await _repositorio.CategoriaTemTransacoes(id))
            return new(["Não é possível remover esta categoria porque existem transações vinculadas a ela."]);

        await _repositorio.RemoverAsync(id);
        return new(Array.Empty<string>());
    }

    private CategoriaRespostaDTO? MapearParaResposta(Categoria categoria) => new()
    {
        Id = categoria!.Id,
        Descricao = categoria.Descricao,
        Finalidade = categoria.Finalidade.ToString()
    };
}
