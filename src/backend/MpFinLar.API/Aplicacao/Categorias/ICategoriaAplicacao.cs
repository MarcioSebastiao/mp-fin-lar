using MpFinLar.API.Dominio.Entidades;

namespace MpFinLar.API.Aplicacao.Categorias;

public interface ICategoriaAplicacao
{
    Task<(CategoriaRespostaDTO?, ResultadoAplicacao)> CriarAsync(CategoriaDto dto);
    Task<IEnumerable<CategoriaRespostaDTO>> ObterAsync();
}
