using System;
using System.Collections.Generic;

class Pedido
{
    private double TAXA_SERVICO = 0.1;
    private int idPedido;
    private Dictionary<int, int> produtos;
    private Cardapio cardapio;

    /// <summary>
    /// Construtor da classe Pedido.
    /// </summary>
    public Pedido()
    {
        Random rand = new Random();
        this.idPedido = rand.Next();
        this.produtos = new Dictionary<int, int>();
        this.cardapio = new Cardapio();
    }

    /// <summary>
    /// Método que adiciona um produto ao pedido.
    /// </summary>
    /// <param name="idProduto">ID do produto.</param>
    /// <param name="quantidade">Quantidade do produto.</param>
    public void AdicionarProduto(int idProduto, int quantidade)
    {
        if (produtos.ContainsKey(idProduto))
        {
            produtos[idProduto] += quantidade;
        }
        else
        {
            produtos.Add(idProduto, quantidade);
        }
    }

    /// <summary>
    /// Método que remove um produto do pedido.
    /// </summary>
    public double CalcularValorConta()
    {
        double total = 0;
        foreach (var produto in produtos)
        {
            Produto infoProduct = cardapio.ObterProduto(produto.Key);

            total += produto.Value * infoProduct.GetValor();
        }

        total += total * TAXA_SERVICO;

        return total;
    }

    /// <summary>
    /// Método que calcula o valor da conta dividido pelo número de pessoas.
    /// </summary>
    /// <param name="numeroPessoas">Número de pessoas.</param>
    /// <param name="total">Valor total da conta.</param>
    public double CalcularDividirConta(int numeroPessoas, double total)
    {
        if (numeroPessoas <= 0)
        {
            throw new ArgumentException("O número de pessoas deve ser maior que zero.");
        }

        return total / numeroPessoas;
    }

    /// <summary>
    /// Método que fecha a conta do pedido.
    /// </summary>
    /// <param name="numeroPessoas">Número de pessoas.</param>
    public double FecharConta(int numeroPessoas)
    {
        double total = this.CalcularValorConta();

        if (numeroPessoas > 0)
        {
            return CalcularDividirConta(numeroPessoas, total);
        }
        else
        {
            return total;
        }
    }
}
