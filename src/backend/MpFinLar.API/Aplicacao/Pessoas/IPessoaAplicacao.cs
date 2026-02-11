namespace MpFinLar.API.Aplicacao.Pessoas;

public interface IPessoaAplicacao
{
    Task<ResultadoAplicacao> CriarAsync(PessoaDTO dto);
    Task<IEnumerable<PessoaRespostaDTO>> ObterAsync();
}
