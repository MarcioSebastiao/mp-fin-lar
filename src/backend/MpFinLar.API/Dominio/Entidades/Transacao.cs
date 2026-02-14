using MpFinLar.API.Dominio.Enums;

namespace MpFinLar.API.Dominio.Entidades;

public sealed class Transacao : Entidade
{
    public string Descricao { get; private set; } = string.Empty;
    public decimal Valor { get; private set; }
    public TipoTrasacao Tipo { get; private set; }

    public Guid CategoriaId { get; private set; }
    public Categoria Categoria { get; private set; } = null!;

    public Guid PessoaId { get; private set; }
    public Pessoa Pessoa { get; private set; } = null!;

    // Construtor privado para uso do Entity Framework 
    private Transacao() { }

    /// <summary>
    /// Cria uma nova instância de Transacao aplicando as regras de negócio.
    /// </summary>
    /// <param name="descricao">
    /// Descrição da transação, com no máximo 400 caracteres.
    /// </param>
    /// <param name="valor">
    /// Valor da transação. Deve ser maior que zero.
    /// </param>
    /// <param name="tipo">
    /// Tipo da transação: Despesa ou Receita.
    /// </param>
    /// <param name="categoria">
    /// Categoria é obrigatória e deve ser compatível com o tipo de transação.
    /// </param>
    /// <param name="pessoa">
    /// Pessoa e obrigatória. E menores de idade só podem ter transações do tipo despesa.
    /// </param>
    /// <remarks>
    /// Caso alguma regra de negócio seja violada, a entidade será criada em estado inválido,
    /// contendo notificações de validação, sem o lançamento de exceções.
    /// </remarks>

    public Transacao(string descricao, decimal valor, TipoTrasacao tipo, Categoria categoria, Pessoa pessoa)
    {
        Validar(descricao, valor, tipo, categoria, pessoa);
        if (!EhValido)
            return;

        Descricao = descricao;
        Valor = valor;
        Tipo = tipo;
        CategoriaId = categoria.Id;
        PessoaId = pessoa.Id;
    }

    private void Validar(string descricao, decimal valor, TipoTrasacao tipo, Categoria categoria, Pessoa pessoa)
    {
        if (!string.IsNullOrWhiteSpace(descricao) && descricao.Length > 400)
            AdicionarNotificacao("A descrição da transação deve ter no máximo 400 caracteres.");

        if (valor <= 0)
            AdicionarNotificacao("O valor da transação deve ser maior que zero.");

        if (!Enum.IsDefined(tipo))
            AdicionarNotificacao("O tipo informado é inválido.");

        if (pessoa is null)
        {
            AdicionarNotificacao("A pessoa é obrigatória.");
            return;
        }

        if (!EhValido)
            return;

        ValidarIdadeParaTipoTrasacao(pessoa, tipo);
        ValidarCategoria(categoria, tipo);
    }

    private void ValidarCategoria(Categoria categoria, TipoTrasacao tipo)
    {
        if (categoria is null)
        {
            AdicionarNotificacao("A categoria é obrigatória.");
            return;
        }

        var compativel = categoria.Finalidade switch
        {
            FinalidadeCategoria.Ambas => true,
            FinalidadeCategoria.Despesa => tipo == TipoTrasacao.Despesa,
            FinalidadeCategoria.Receita => tipo == TipoTrasacao.Receita,
            _ => false
        };

        if (!compativel)
            AdicionarNotificacao("A finalidade da categoria não pode ser diferente do tipo de transação.");
    }

    /// <summary>
    /// Se for menor de idade só poderá ter despesa
    /// </summary>
    private void ValidarIdadeParaTipoTrasacao(Pessoa pessoa, TipoTrasacao tipoTrasacao)
    {
        if (pessoa.Idade < 18 && tipoTrasacao == TipoTrasacao.Receita)
            AdicionarNotificacao("Menores de idade só podem possuir despesas.");
    }
}
