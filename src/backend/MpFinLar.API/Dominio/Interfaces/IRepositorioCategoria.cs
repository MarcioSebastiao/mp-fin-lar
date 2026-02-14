using MpFinLar.API.Aplicacao.Categorias;
using MpFinLar.API.Dominio.Entidades;

namespace MpFinLar.API.Dominio.Interfaces;

public interface IRepositorioCategoria
{
    Task AdicionarAsync(Categoria categoria);
    Task<IEnumerable<CategoriaRespostaDTO>> ObterCategoriasAsync();
}
