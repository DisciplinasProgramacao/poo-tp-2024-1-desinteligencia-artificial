/// <summary>
/// Classe que representa o cardápio do restaurante.
/// </summary>
class Cardapio
{
    private List<Produto> produtos;

    /// <summary>
    /// Método construtor da classe Cardapio. Inicializa a lista de produtos com opções predefinidas.
    /// </summary>
    public Cardapio(List<Produto> produtos)
    {
        this.produtos = produtos;
    }

    /// <summary>
    /// Mostra todas as opções do cardápio.
    /// </summary>
    public string MostrarCardapio()
    {
        string cardapio = "Cardápio:\n";

        foreach (var produto in produtos)
        {
            cardapio += $"{produto.GetId()}. {produto.GetNome()} – R$ {produto.GetValor()}\n";
        }

        return cardapio;
    }

    /// <summary>
    /// Retorna um produto baseado no ID.
    /// </summary>
    /// <param name="id">ID do produto.</param>
    /// <returns>O produto correspondente ao ID, ou null se não for encontrado.</returns>
    public Produto? ObterProduto(int id)
    {
        Produto? produto = produtos.Find(produto => produto.GetId() == id);
        return produto;
    }
}