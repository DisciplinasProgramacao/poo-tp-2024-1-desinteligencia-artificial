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
    /// <summary>
    /// Aloca uma mesa específica para uma requisição se disponível.
    /// </summary>
    /// <param name="req">A requisição de mesa.</param>
    /// <param name="mesa">A mesa a ser alocada.</param>
    /// <returns>True se a mesa foi alocada com sucesso; caso contrário, False.</returns>
    protected override bool AlocarMesa(ReqMesa req, Mesa mesa)
    {
        if (mesa.VerificarDisponibilidade(req.QtdPessoas))
        {
            mesa.OcuparMesa();
            listaRegistros.Add(req);
            return true;
        }

        return false;
    }

    /// <summary>
    /// Processa uma requisição de mesa, alocando uma mesa disponível ou adicionando à lista de espera.
    /// </summary>
    /// <param name="req">A requisição de mesa.</param>
    /// <returns>True se a mesa foi alocada com sucesso; caso contrário, False.</returns>
    public override bool ProcessarRequisicao(ReqMesa req)
    {
        foreach (Mesa mesa in mesas)
        {
            if (AlocarMesa(req, mesa))
            {
                return true;
            }
        }

        return false;
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