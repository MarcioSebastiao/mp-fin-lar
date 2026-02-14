using MpFinLar.API.Dominio.Entidades;

namespace MpFinLar.API.Aplicacao.Pessoas;

public interface IPessoaAplicacao
{
    Task<(PessoaRespostaDTO?, ResultadoAplicacao)> CriarAsync(PessoaDTO dto);
    Task<(PessoaRespostaDTO?, ResultadoAplicacao)> AtualizarAsync(Guid Id, PessoaDTO dto);
    Task<ResultadoAplicacao> RemoverAsync(Guid id);
    Task<IEnumerable<PessoaRespostaDTO>> ObterAsync();
}
