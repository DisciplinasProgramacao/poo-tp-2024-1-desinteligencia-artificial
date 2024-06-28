using System;
using System.Collections.Generic;

class Cafe : Loja
{

    public Cafe()
    {
        CriarMesa(4);
        CriarMesa(4);
        CriarMesa(4);
        CriarMesa(4);
        CriarMesa(6);
        CriarMesa(6);
        CriarMesa(6);
        CriarMesa(6);
        CriarMesa(8);
        CriarMesa(8);
    }

    public void AdicionarRequisicao(ReqMesa req)
    {
        if (req.IdMesa != null)
        {
            Mesa mesa = PesquisarMesa(req.IdMesa.Value);
            mesa.OcuparMesa();
        }

        req.IniciarRequisicao();
        listaRegistros.Add(req);
    }

    /// <summary>
    /// Fecha a mesa especificada pelo ID, desocupando-a e retornando o valor da conta.
    /// </summary>
    /// <param name="nomeCliente">Nome do cliente que deseja fechar a conta.</param>
    /// <returns>O valor da conta se a mesa foi fechada com sucesso; caso contrário, retorna -1.</returns>
    public string FecharConta(string nomeCliente)
    {
        ReqMesa? req = ObterRequisicaoPorCliente(nomeCliente);
        if (req == null)
        {
            return "Pedido do cliente não encontrado.";
        }

        if (req.IdMesa != null)
        {
            Mesa? mesa = PesquisarMesa(req.IdMesa.Value);
            mesa.DesocuparMesa();
        }

        return req.FecharRequisicao(false);
    }

    protected override Cardapio CriarCardapio()
    {
        List<Produto> produtos = new List<Produto>
        {
            new Produto(1, "Não de queijo", 5, "Delicioso pão com recheio cremoso de queijo derretido"),
            new Produto(2, "Bolinha de cogumelo", 7, "Bolinhas crocantes recheadas com cogumelos frescos e temperos especiais"),
            new Produto(3, "Rissole de palmito", 7, "Massa crocante recheada com palmito macio e temperos selecionados"),
            new Produto(4, "Coxinha de carne de jaca", 8, "Coxinha vegana com recheio suculento de carne de jaca desfiada"),
            new Produto(5, "Fatia de queijo de caju", 9, "Fatia generosa de queijo vegano feito com castanha de caju"),
            new Produto(6, "Biscoito amanteigado", 3, "Biscoito caseiro amanteigado, perfeito para acompanhar um café"),
            new Produto(7, "Cheesecake de frutas vermelhas", 15, "Deliciosa cheesecake com uma combinação irresistível de frutas vermelhas"),
            new Produto(8, "Água", 3, "Água mineral pura e refrescante"),
            new Produto(9, "Copo de suco", 7, "Copo de suco natural feito com frutas frescas e sem adição de açúcar"),
            new Produto(10, "Café espresso orgânico", 6, "Café espresso de alta qualidade, cultivado de forma orgânica e com aroma intenso")
        };

        cardapio = new Cardapio(produtos);
        return cardapio;
    }
}