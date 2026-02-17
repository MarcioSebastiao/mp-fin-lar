using MpFinLar.API.Aplicacao.Transacoes;
using MpFinLar.API.Dominio.Entidades;
using MpFinLar.API.Dominio.Modelos;

namespace MpFinLar.API.Dominio.Interfaces;

public interface IRepositorioTransacao
{
    Task AdicionarAsync(Transacao transacao);

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
    /// Quantidade máxima de itens a serem retornados.
    /// </param>
    /// <returns>
    /// Coleção de transações da pessoa no formato de resposta.
    /// </returns>
    Task<IEnumerable<TransacaoRespostaDTO>> ObterTransacoesDePessoaAsync(Guid pessoaId, int pularItens, int quantidadeItens);
    Task<int> ObterTotalDeTransacoesDePessoaAsync(Guid pessoaId);
    Task<ValoresTransacoes> ObterValoresDePessoaAsync(Guid pessoaId);
    Task<ValoresTransacoes> ObterValoresPorCategoriaAsync(Guid categoriaId);
}
