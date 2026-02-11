using MpFinLar.API.Aplicacao.Pessoas;
using MpFinLar.API.Dominio.Entidades;

namespace MpFinLar.API.Dominio.Interfaces;

public interface IRepositorioPessoa
{
    Task AdicionarAsync(Pessoa pessoa);
    Task<bool> AtualizarAsync(Pessoa pessoa);
    Task<Pessoa?> ObterPorIdAsync(Guid id);
    Task<IEnumerable<PessoaRespostaDTO>> ObterPessoasAsync();
    Task<bool> RemoverAsync(Guid id);
}
