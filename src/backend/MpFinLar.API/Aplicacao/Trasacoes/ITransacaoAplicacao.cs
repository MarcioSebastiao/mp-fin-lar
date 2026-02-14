using MpFinLar.API.Dominio.Entidades;

namespace MpFinLar.API.Aplicacao.Transacoes;

public interface ITransacaoAplicacao
{
    Task<(TransacaoRespostaDTO?, ResultadoAplicacao)> CriarAsync(TransacaoDto dto);
    Task<IEnumerable<TransacaoRespostaDTO>> ObterAsync();
}
