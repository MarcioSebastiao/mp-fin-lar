namespace MpFinLar.API.Dominio.Entidades;

/// <summary>
/// Entidade base do domínio.
/// 
/// O identificador é gerado no domínio usando GUID versão 7 para:
/// - Evitar dependência do banco de dados (Clean Architecture)
/// - Permitir criação de entidades fora da infraestrutura
/// - Melhorar performance de índices por ser ordenável por tempo
/// 
/// Centraliza comportamentos comuns, incluindo a validação de regras de negócio
/// por meio do Padrão Notification, para evitar exceções no domínio.
/// 
/// </summary>
public abstract class Entidade 
{
    public Guid Id { get; protected set; }

    private readonly List<string> _notificacoes = new();

    /// <summary>
    /// Lista de notificações geradas durante a validação das regras de negócio.
    /// </summary>
    public IReadOnlyCollection<string> Notificacoes => _notificacoes;

    /// <summary>
    /// Indica se a entidade está em um estado válido,
    /// ou seja, se não possui notificações registradas.
    /// </summary>
    public bool EhValido => !_notificacoes.Any();

    public Entidade()
    {
        Id = Guid.CreateVersion7();
    }

    /// <summary>
    /// Adiciona uma notificação de validação à entidade.
    /// </summary>
    /// <param name="mesagem">
    /// Mensagem que descreve a violação de regra de negócio. 
    /// </param>
    public void AdicionarNotificacao(string mesagem)
    {
        _notificacoes.Add(mesagem);
    }

}
