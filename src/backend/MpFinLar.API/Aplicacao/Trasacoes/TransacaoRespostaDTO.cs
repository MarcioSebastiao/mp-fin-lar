using MpFinLar.API.Aplicacao.Categorias;
using MpFinLar.API.Aplicacao.Pessoas;

namespace MpFinLar.API.Aplicacao.Transacoes;

public sealed class TransacoesRespostaDTO
{
    public IEnumerable<TransacaoRespostaDTO> Transacoes { get; set; } = Enumerable.Empty<TransacaoRespostaDTO>();
    public ValoresTransacaoRespostaDTO Valores { get; set; } = new ValoresTransacaoRespostaDTO();
}

public sealed class TransacaoRespostaDTO
{
    public Guid Id { get; set; }
    public string Descricao { get; set; } = string.Empty;
    public decimal Valor { get; set; }
    public string Tipo { get; set; } = string.Empty;
    public CategoriaRespostaDTO? Categoria { get; set; }
    public PessoaRespostaDTO? Pessoa { get; set; }

}

public sealed class ValoresTransacaoRespostaDTO
{
    public decimal TotalEmDespesas { get; set; }
    public decimal TotalEmReceitas { get; set; }
    public decimal Saldo { get; set; }
}

