namespace MpFinLar.API.Aplicacao;

public abstract class RespostaDTO
{
    public Guid? Id { get; set; }
    public DateTimeOffset? DataCriacao { get; set; }
}
