using MpFinLar.API.Aplicacao.Categorias;
using MpFinLar.API.Dominio.Entidades;

namespace MpFinLar.API.Dominio.Interfaces;

public interface IRepositorioCategoria
{
    Task AdicionarAsync(Categoria categoria);
    Task<IEnumerable<CategoriaRespostaDTO>> ObterCategoriasAsync(int pularItens, int quantidadeItens);
    Task<int> ObterTotalDeItensAsync();
    Task<Categoria?> ObterPorIdAsync(Guid id);
    Task<bool> CategoriaTemTransacoes(Guid id);
    Task RemoverAsync(Guid id);
}
