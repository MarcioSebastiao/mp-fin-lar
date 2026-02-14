using MpFinLar.API.Dominio.Enums;

namespace MpFinLar.API.Aplicacao.Categorias;

public sealed class CategoriaRespostaDTO
{
    public Guid Id { get; set; }
    public string Descricao { get; set; } = string.Empty;
    public string Finalidade { get; set; } = string.Empty;
}
