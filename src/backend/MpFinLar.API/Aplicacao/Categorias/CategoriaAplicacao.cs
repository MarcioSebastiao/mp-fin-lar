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

    public async Task<(Categoria?, ResultadoAplicacao)> CriarAsync(CategoriaDto dto)
    {
        var categoria = new Categoria(dto.Descricao, dto.Finalidade);

        if (!categoria.EhValido)
            return new(null, new(categoria.Notificacoes));

        await _repositorio.AdicionarAsync(categoria);

        return new(categoria, new(Array.Empty<string>()));
    }

    public Task<IEnumerable<CategoriaRespostaDTO>> ObterAsync() => _repositorio.ObterCategoriasAsync();
}
