namespace MpFinLar.API.Aplicacao.Pessoas;

public interface IPessoaAplicacao
{
    Task<ResultadoAplicacao> CriarAsync(PessoaDTO dto);
    Task<ResultadoAplicacao> AtualizarAsync(Guid Id, PessoaDTO dto);
    Task<ResultadoAplicacao> RemoverAsync(Guid id);
    Task<IEnumerable<PessoaRespostaDTO>> ObterAsync();
}
