using System.ComponentModel.DataAnnotations;

namespace MpFinLar.API.Aplicacao.Pessoas;

public class PessoaDTO
{
    [Required(ErrorMessage = "O nome é obrigatório.")]
    [MaxLength(200, ErrorMessage = "O nome deve ter no máximo 200 caracteres.")]
    public string Nome { get; set; } = string.Empty;
    
    [Range(0, 200, ErrorMessage = "Informe uma idade valida.")]
    public int Idade { get; set; }
}
