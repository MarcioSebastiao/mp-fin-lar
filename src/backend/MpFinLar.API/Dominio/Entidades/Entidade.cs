namespace MpFinLar.API.Dominio.Entidades;

/// <summary>
/// Entidade base do domínio.
/// 
/// O identificador é gerado no domínio usando GUID versão 7 para:
/// - Evitar dependência do banco de dados (Clean Architecture)
/// - Permitir criação de entidades fora da infraestrutura
/// - Melhorar performance de índices por ser ordenável por tempo
/// 
/// </summary>
public abstract class Entidade
{
    public Guid Id { get; protected set; }

    public Entidade()
    {
        Id = Guid.CreateVersion7();
    }
}
