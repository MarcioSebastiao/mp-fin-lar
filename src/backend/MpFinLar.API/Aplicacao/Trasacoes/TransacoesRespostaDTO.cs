namespace MpFinLar.API.Aplicacao.Transacoes;

public sealed class TransacoesRespostaDTO
{
    public IEnumerable<TransacaoRespostaDTO> Transacoes { get; set; } = Enumerable.Empty<TransacaoRespostaDTO>();
    public ValoresTransacaoRespostaDTO Valores { get; set; } = new ValoresTransacaoRespostaDTO();
    public int TotalDeItens { get; set; }
}

