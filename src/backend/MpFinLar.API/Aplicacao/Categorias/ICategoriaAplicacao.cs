using MpFinLar.API.Dominio.Entidades;

namespace MpFinLar.API.Aplicacao.Categorias;

public interface ICategoriaAplicacao
{
    Task<(CategoriaRespostaDTO?, ResultadoAplicacao)> CriarAsync(CategoriaDto dto);

    /// <summary>
    /// Obtém uma lista de categorias, aplicando paginação por deslocamento.
    /// </summary>
    /// <param name="pularItens">
    /// Quantidade de registros a serem ignorados.
    /// </param>
    /// <param name="quantidadeItens">
    /// Quantidade máxima de itens a serem retornados. O valor padrão e máximo permitido é 100.
    /// </param>
    /// <returns>
    /// Coleção de categorias no formato de resposta.
    /// </returns>
    Task<CategoriasRespostaDTO> ObterAsync(int pularItens = 0, int quantidadeItens = 100);
}
