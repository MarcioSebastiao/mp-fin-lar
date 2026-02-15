namespace MpFinLar.API.Aplicacao.Pessoas;

public sealed class PessoasRespostaDTO
{
    public IEnumerable<PessoaRespostaDTO> Pessoas { get; set; } = Enumerable.Empty<PessoaRespostaDTO>();
    public int TotalDeItens { get; set; }
}