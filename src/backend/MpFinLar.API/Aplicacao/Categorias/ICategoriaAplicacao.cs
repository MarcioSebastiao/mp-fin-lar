using MpFinLar.API.Dominio.Entidades;

namespace MpFinLar.API.Aplicacao.Categorias;

public interface ICategoriaAplicacao
{
    Task<(Categoria?, ResultadoAplicacao)> CriarAsync(CategoriaDto dto);
    Task<IEnumerable<CategoriaRespostaDTO>> ObterAsync();
}
