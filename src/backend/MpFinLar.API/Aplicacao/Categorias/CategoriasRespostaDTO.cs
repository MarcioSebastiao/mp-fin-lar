namespace MpFinLar.API.Aplicacao.Categorias;

public sealed class CategoriasRespostaDTO
{
    public IEnumerable<CategoriaRespostaDTO> Categorias { get; set; } = Enumerable.Empty<CategoriaRespostaDTO>();
    public int TotalDeItens { get; set; }
}
