namespace MpFinLar.API.Aplicacao.Transacoes;

public sealed class ValoresTransacaoRespostaDTO
{
    public decimal TotalEmDespesas { get; set; }
    public decimal TotalEmReceitas { get; set; }
    public decimal Saldo { get; set; }
}

