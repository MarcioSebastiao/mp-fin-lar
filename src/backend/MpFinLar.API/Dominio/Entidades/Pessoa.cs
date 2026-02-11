namespace MpFinLar.API.Dominio.Entidades;

public sealed class Pessoa : Entidade
{
    public string Nome { get; private set; } = string.Empty;
    public int Idade { get; private set; }

    /// <summary>
    /// Cria uma nova instância de Pessoa aplicando as regras de negócio
    /// </summary>
    /// <param name="nome">
    /// Nome da pessoa. Deve ser obrigatório e possuir no máximo 200 caracteres.
    /// </param>
    /// <param name="idade">
    /// Idade da pessoa.
    /// </param>
    /// <remarks>
    /// Caso alguma regra de negócio seja violada, a entidade será criada em estado inválido,
    /// contendo notificações de validação, sem o lançamento de exceções.
    /// </remarks>
    public Pessoa(string nome, int idade)
    {
        Idade = idade;

        Validar(nome);

        if (EhValido)
            Nome = nome;
    }

    public void Atualizar(string nome, int idade)
    {
        Validar(nome);

        if (EhValido)
        {
            Nome = nome;
            Idade = idade;
        }
    }

    private void Validar(string nome)
    {
        if (string.IsNullOrWhiteSpace(nome))
            AdicionarNotificacao("O nome da Pessoa é obrigatorio.");

        if (nome.Length > 200)
            AdicionarNotificacao("O nome da Pessoa deve ter no máximo 200 caracteres.");
    }
}
