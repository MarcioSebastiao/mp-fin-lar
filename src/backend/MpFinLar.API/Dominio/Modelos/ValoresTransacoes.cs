namespace MpFinLar.API.Dominio.Modelos;

public sealed class ValoresTransacoes
{
    public decimal TotalEmDespesas { get; private set; }
    public decimal TotalEmReceitas { get; private set; }
    public decimal Saldo { get; private set; }

    public ValoresTransacoes(decimal totalEmDespesas, decimal totalEmReceitas)
    {
        TotalEmDespesas = totalEmDespesas;
        TotalEmReceitas = totalEmReceitas;
        Saldo = totalEmReceitas - totalEmDespesas;
    }
}