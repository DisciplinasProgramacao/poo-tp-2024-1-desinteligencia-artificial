using System;
using System.Collections.Generic;

class Pedido
{
    private double TAXA_SERVICO = 0.1;
    private int idPedido;
    private Dictionary<int, int> produtos;

    public Pedido()
    {
        Random rand = new Random();
        this.idPedido = rand.Next();
    }

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

    public double CalcularValorConta(Dictionary<int, double> precosProdutos)
    {
        double total = 0;
        foreach (var produto in produtos)
        {
            if (precosProdutos.ContainsKey(produto.Key))
            {
                total += produto.Value * precosProdutos[produto.Key];
            }
        }

        total += total * TAXA_SERVICO;

        return total;
    }

    public double CalcularDividirConta(int numeroPessoas, double total)
    {
        if (numeroPessoas <= 0)
        {
            throw new ArgumentException("O número de pessoas deve ser maior que zero.");
        }

        return total / numeroPessoas;
    }

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
