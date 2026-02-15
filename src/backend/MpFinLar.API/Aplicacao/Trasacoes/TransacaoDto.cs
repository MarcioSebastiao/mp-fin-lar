using System.ComponentModel.DataAnnotations;
using MpFinLar.API.Dominio.Enums;

namespace MpFinLar.API.Aplicacao.Transacoes;

public sealed class TransacaoDto
{
    [MaxLength(400, ErrorMessage = "A descrição da transação deve ter no máximo 400 caracteres.")]
    public string Descricao { get; set; } = string.Empty;

    [Range(0.01, double.MaxValue, ErrorMessage = "O valor da transação deve ser maior que zero.")]
    public decimal Valor { get; set; }

    [EnumDataType(typeof(TipoTrasacao), ErrorMessage = "O tipo informado é inválido.")]
    public TipoTrasacao Tipo { get; set; }

    [Required(ErrorMessage = "A categoria é obrigatória.")]
    public Guid CategoriaId { get; set; }

    [Required(ErrorMessage = "A pessoa é obrigatória.")]
    public Guid PessoaId { get; set; }
}
