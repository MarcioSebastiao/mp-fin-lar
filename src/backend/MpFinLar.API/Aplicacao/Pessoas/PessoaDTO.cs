using System.ComponentModel.DataAnnotations;

namespace MpFinLar.API.Aplicacao.Pessoas;

public class PessoaDTO
{
    [Required(ErrorMessage = "O nome é obrigatório.")]
    public string Nome { get; set; } = string.Empty;
    public int Idade { get; set; }
}
