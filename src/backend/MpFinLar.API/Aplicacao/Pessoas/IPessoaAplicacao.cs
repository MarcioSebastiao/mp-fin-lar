namespace MpFinLar.API.Aplicacao.Pessoas;

public interface IPessoaAplicacao
{
    Task<(PessoaRespostaDTO?, ResultadoAplicacao)> CriarAsync(PessoaDTO dto);
    Task<(PessoaRespostaDTO?, ResultadoAplicacao)> AtualizarAsync(Guid Id, PessoaDTO dto);
    Task<ResultadoAplicacao> RemoverAsync(Guid id);

    /// <summary>
    /// Obtém uma lista de pessoas, aplicando paginação por deslocamento.
    /// </summary>
    /// <param name="pularItens">
    /// Quantidade de registros a serem ignorados.
    /// </param>
    /// <param name="quantidadeItens">
    /// Quantidade máxima de itens a serem retornados. O valor padrão e máximo permitido é 100.
    /// </param>
    /// <returns>
    /// Coleção de pessoas no formato de resposta.
    /// </returns>
    Task<PessoasRespostaDTO> ObterAsync(int pularItens = 0, int quantidadeItens = 100);
}
