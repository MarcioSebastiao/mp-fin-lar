namespace MpFinLar.API.Aplicacao.Pessoas;

public sealed class PessoaRespostaDTO
{
    public Guid Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public int Idade { get; set; }
}