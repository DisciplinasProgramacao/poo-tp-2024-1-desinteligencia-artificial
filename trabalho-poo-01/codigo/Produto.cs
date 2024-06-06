/// <summary>
/// Classe que representa um produto no cardápio do restaurante.
/// </summary>
class Produto
{
    // Atributos privados da classe Produto.
    private int id { get; private set; }
    private string nome { get; private set; }
    private double valor { get; private set; }
    private string descricao { get; private set; }

    /// <summary>
    /// Método construtor da classe Produto.
    /// </summary>
    /// <param name="id">ID do produto.</param>
    /// <param name="nome">Nome do produto.</param>
    /// <param name="valor">Valor do produto.</param>
    /// <param name="descricao">Descrição do produto.</param>
    public Produto(int id, string nome, double valor, string descricao)
    {
        this.id = id;
        this.nome = nome;
        this.valor = valor;
        this.descricao = descricao;
    }

    /// <summary>
    /// Método que retorna o valor do produto.
    /// </summary>
    public double GetValor()
    {
        return this.valor;
    }
}