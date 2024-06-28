using System;
using System.Collections.Generic;

/// <summary>
/// Classe que representa um restaurante, gerenciando mesas, requisições, clientes e cardápio.
/// </summary>
class Restaurante : Loja
{
    private List<ReqMesa> listaEspera;

    /// <summary>
    /// Inicializa uma nova instância da classe <see cref="Restaurante"/>, criando mesas e listas necessárias.
    /// </summary>
    public Restaurante()
    {
        this.listaEspera = new List<ReqMesa>();

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
    private bool AlocarMesa(ReqMesa req, Mesa mesa)
    {
        if (mesa.VerificarDisponibilidade(req.QtdPessoas))
        {
            mesa.OcuparMesa();
            req.AtribuirMesaARequisicao(mesa.NumeroMesa);
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
    public bool ProcessarRequisicao(ReqMesa req)
    {
        foreach (Mesa mesa in mesas)
        {
            if (AlocarMesa(req, mesa))
            {
                return true;
            }
        }

        listaEspera.Add(req);
        return false;
    }

    /// <summary>
    /// Processa a lista de espera, alocando mesas conforme disponibilidade.
    /// </summary>
    /// <returns>String indicando as requisições processadas com sucesso.</returns>
    public string ProcessarListaDeEspera()
    {
        string resposta = "";
        List<ReqMesa> reqAExcluir = new List<ReqMesa>();

        foreach (ReqMesa req in listaEspera)
        {
            foreach (Mesa mesa in mesas)
            {
                if (AlocarMesa(req, mesa))
                {
                    reqAExcluir.Add(req);
                    resposta += $"Requisição {req.IdReq} alocada com sucesso.\n";
                    break;
                }
            }
        }

        foreach (ReqMesa req in reqAExcluir)
        {
            listaEspera.Remove(req);
        }

        if (resposta == "")
        {
            resposta = "Nenhuma requisição foi alocada.";
        }

        return resposta;
    }

    public string FecharConta(int idMesa)
    {
        ReqMesa? req = ObterRequisicaoPorMesa(idMesa);
        Mesa? mesa = mesas.Find(mesa => mesa.NumeroMesa == idMesa);
        if (mesa != null && req != null)
        {
            if (!mesa.EstaOcupada)
            {
                return "A mesa não está ocupada!";
            }

            mesa.DesocuparMesa();
            return req.FecharRequisicao(true);
        }

        return "Mesa não encontrada!";
    }

    /// <summary>
    /// Lista as requisições em espera.
    /// </summary>
    /// <returns>String contendo as requisições em espera.</returns>
    public string ListarFilaDeEspera()
    {

        string lista = "Lista de fila de espera: \n";

        if (listaEspera.Count == 0)
        {
            return "Não há requisições em espera.";
        }

        foreach (ReqMesa req in listaEspera)
        {
            lista += $"Cliente: {req.NomeCliente}, Quantidade de pessoas: {req.QtdPessoas} \n";
        }

        return lista;
    }

    protected override Cardapio CriarCardapio()
    {
        List<Produto> produtos = new List<Produto>
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

        cardapio = new Cardapio(produtos);
        return cardapio;
    }

}
