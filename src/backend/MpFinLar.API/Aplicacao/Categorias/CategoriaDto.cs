using System.ComponentModel.DataAnnotations;
using MpFinLar.API.Dominio.Enums;

namespace MpFinLar.API.Aplicacao.Categorias;

public sealed class CategoriaDto
{

    [Required]
    [MaxLength(400, ErrorMessage = "A descrição deve conter no máximo 400 caracteres.")]
    public string Descricao { get; set; } = string.Empty;
    public FinalidadeCategoria Finalidade { get; set; } = 0;
}

