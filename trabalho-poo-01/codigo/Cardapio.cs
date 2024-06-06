/// <summary>
/// Classe que representa o cardápio do restaurante.
/// </summary>
class Cardapio
{
    private List<Produto> produtos;

    /// <summary>
    /// Método construtor da classe Cardapio. Inicializa a lista de produtos com opções predefinidas.
    /// </summary>
    public Cardapio()
    {
        produtos = new List<Produto>
        {
            new Produto(1, "Moqueca de Palmito", 32.00, "Moqueca vegana com palmito, leite de coco, azeite de dendê, pimentões, tomates e coentro."),
            new Produto(2, "Falafel Assado", 20.00, "Bolinhos de grão-de-bico assados, temperados com ervas e especiarias."),
            new Produto(3, "Salada Primavera com Macarrão Konjac", 25.00, "Salada fresca com macarrão konjac e vegetais da primavera."),
            new Produto(4, "Escondidinho de Inhame", 18.00, "Purê de inhame com recheio de vegetais e cogumelos."),
            new Produto(5, "Strogonoff de Cogumelos", 30.00, "Cogumelos em molho cremoso de leite de coco ou creme de castanhas."),
            new Produto(6, "Caçarola de legumes", 24.00, "Vegetais variados assados em molho de tomate e ervas."),
            new Produto(7, "Água", 3.00, "Bebida pura e fresca."),
            new Produto(8, "Copo de suco", 7.00, "Suco natural de frutas frescas."),
            new Produto(9, "Refrigerante orgânico", 7.00, "Refrigerante feito com ingredientes orgânicos."),
            new Produto(10, "Cerveja vegana", 12.00, "Cerveja sem produtos de origem animal."),
            new Produto(11, "Taça de vinho vegano", 29.00, "Vinho produzido sem clarificantes de origem animal.")
        };
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
    public Produto ObterProduto(int id)
    {
        return produtos.Find(p => p.GetId() == id);
    }
}