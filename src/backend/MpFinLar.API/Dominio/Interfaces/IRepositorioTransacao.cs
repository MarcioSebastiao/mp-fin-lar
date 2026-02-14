using MpFinLar.API.Aplicacao.Transacoes;
using MpFinLar.API.Dominio.Entidades;

namespace MpFinLar.API.Dominio.Interfaces;

public interface IRepositorioTransacao
{
    Task AdicionarAsync(Transacao transacao);
    Task<IEnumerable<TransacaoRespostaDTO>> ObterTransacoesAsync();
}
