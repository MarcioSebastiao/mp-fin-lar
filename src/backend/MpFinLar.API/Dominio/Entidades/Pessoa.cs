namespace MpFinLar.API.Dominio.Entidades;

public sealed class Pessoa : Entidade
{
    public string Nome { get; private set; }
    public int Idade { get; private set; }

    public Pessoa(string nome, int idade)
    {
        Nome = nome;
        Idade = idade;
    }
}
