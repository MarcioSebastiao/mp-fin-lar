using MpFinLar.API.Dominio.Enums;

namespace MpFinLar.API.Dominio.Entidades;

public sealed class Categoria : Entidade
{
    public string Descricao { get; private set; } = string.Empty;
    public FinalidadeCategoria Finalidade { get; private set; }

    /// <summary>
    /// Cria uma nova instância de Categoria aplicando as regras de negócio.
    /// </summary>
    /// <param name="descricao">
    /// Descrição da categoria. Obrigatória e com no máximo 400 caracteres.
    /// </param>
    /// <param name="finalidade">
    /// Finalidade da categoria: Despesa, Receita ou Ambas.
    /// </param>
    /// <remarks>
    /// Caso alguma regra de negócio seja violada, a entidade será criada em estado inválido,
    /// contendo notificações de validação, sem o lançamento de exceções.
    /// </remarks>
    public Categoria(string descricao, FinalidadeCategoria finalidade)
    {
        Validar(descricao, finalidade);

        if (EhValido)
        {
            Descricao = descricao;
            Finalidade = finalidade;
        }
    }

    public void Atualizar(string descricao, FinalidadeCategoria finalidade)
    {
        Validar(descricao, finalidade);

        if (EhValido)
        {
            Descricao = descricao;
            Finalidade = finalidade;
        }
    }

    private void Validar(string descricao, FinalidadeCategoria finalidade)
    {
        if (string.IsNullOrWhiteSpace(descricao))
            AdicionarNotificacao("A descrição da categoria é obrigatória.");

        if (!string.IsNullOrWhiteSpace(descricao) && descricao.Length > 400)
            AdicionarNotificacao("A descrição da categoria deve ter no máximo 400 caracteres.");

        if (!Enum.IsDefined(finalidade))
            AdicionarNotificacao("A finalidade informada é inválida.");
    }
}
