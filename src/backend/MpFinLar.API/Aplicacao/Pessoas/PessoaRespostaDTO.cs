namespace MpFinLar.API.Aplicacao.Pessoas;

public sealed class PessoaRespostaDTO: RespostaDTO
{
    public string Nome { get; set; } = string.Empty;
    public int Idade { get; set; }
}