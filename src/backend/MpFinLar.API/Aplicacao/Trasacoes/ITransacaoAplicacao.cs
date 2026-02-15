namespace MpFinLar.API.Aplicacao.Transacoes;

public interface ITransacaoAplicacao
{
    Task<(TransacaoRespostaDTO?, ResultadoAplicacao)> CriarAsync(TransacaoDto dto);

    /// <summary>
    /// Obtém as transações associadas a uma pessoa, aplicando paginação por deslocamento.
    /// </summary>
    /// <param name="pessoaId">
    /// Id da pessoa cujas transações serão consultadas.
    /// </param>
    /// <param name="pularItens">
    /// Quantidade de registros a serem ignorados.
    /// </param>
    /// <param name="quantidadeItens">
    /// Quantidade máxima de itens a serem retornados. O valor padrão e máximo permitido é 100.
    /// </param>
    /// <returns>
    /// Coleção de transações da pessoa no formato de resposta.
    /// </returns>
    Task<TransacoesRespostaDTO> ObterTransacoesDePessoaAsync(Guid pessoaId, int pularItens = 0, int quantidadeItens = 100);
}
