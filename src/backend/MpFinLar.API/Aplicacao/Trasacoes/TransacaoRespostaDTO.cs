using MpFinLar.API.Aplicacao.Categorias;
using MpFinLar.API.Aplicacao.Pessoas;
using MpFinLar.API.Dominio.Enums;

namespace MpFinLar.API.Aplicacao.Transacoes;

public sealed class TransacaoRespostaDTO
{
    public Guid Id { get; set; }
    public decimal Valor { get; set; }
    public TipoTrasacao Tipo { get; set; }
    public CategoriaRespostaDTO Categoria { get; set; } = null!;
    public PessoaRespostaDTO Pessoa { get; set; } = null!;
}
