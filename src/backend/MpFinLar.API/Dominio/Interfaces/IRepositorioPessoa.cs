using MpFinLar.API.Aplicacao.Pessoas;
using MpFinLar.API.Dominio.Entidades;

namespace MpFinLar.API.Dominio.Interfaces;

public interface IRepositorioPessoa
{
    Task AdicionarAsync(Pessoa pessoa);
    Task<IEnumerable<PessoaRespostaDTO>> ObterPessoasAsync();
    Task<bool> RemoverAsync(Guid id);


}
