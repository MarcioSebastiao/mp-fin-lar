namespace MpFinLar.API.Aplicacao;

/// <summary>
/// Classe responsável por guardar as notificações em caso de falha.
/// Será usada para retornar essas notificações na aplicação.
/// </summary>
public sealed class ResultadoAplicacao
{
    public IReadOnlyCollection<string> Notificacoes { get; }
    public bool Sucesso => !Notificacoes.Any();

    public ResultadoAplicacao(IEnumerable<string> notificacoes)
    {
        Notificacoes = Notificacoes = notificacoes.ToList();
    }
}
