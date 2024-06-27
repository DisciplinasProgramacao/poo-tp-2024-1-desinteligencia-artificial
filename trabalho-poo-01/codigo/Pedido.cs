using System;
using System.Collections.Generic;

class Pedido
{
    private double TAXA_SERVICO = 0.1;
    private int idPedido;
    private Dictionary<Produto, int> produtos;

    /// <summary>
    /// Construtor da classe Pedido.
    /// </summary>
    public Pedido()
    {
        Random rand = new Random();
        this.idPedido = rand.Next();
        this.produtos = new Dictionary<Produto, int>();
    }

    /// <summary>
    /// Método que adiciona um produto ao pedido.
    /// </summary>
    /// <param name="produto">Produto.</param>
    /// <param name="quantidade">Quantidade do produto.</param>
    public void AdicionarProduto(Produto produto, int quantidade)
    {
        if (produtos.ContainsKey(produto))
        {
            produtos[produto] += quantidade;
        }
        else
        {
            produtos.Add(produto, quantidade);
        }
    }

    /// <summary>
    /// Método que remove um produto do pedido.
    /// </summary>
    private double CalcularValorConta()
    {
        double total = 0;
        foreach (var item in produtos)
        {
            total += item.Value * item.Key.GetValor();
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
    /// <returns> Valor total do pedido e valor dividido para a quantidade de pessoas.</returns>      
    public string FecharConta(int numeroPessoas)
{
    double total = this.CalcularValorConta();
    string conta = $"Valor total da conta {total}";

    if (numeroPessoas > 1)
    {
        double totalDividido = CalcularDividirConta(numeroPessoas, total);
        return $"{conta} - Valor da conta por pessoa: {totalDividido}";
    }
    else
    {
        return conta;
    }
}

}