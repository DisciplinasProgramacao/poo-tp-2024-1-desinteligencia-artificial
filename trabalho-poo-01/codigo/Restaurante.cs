using System;
using System.Collections.Generic;

/// <summary>
/// Classe que representa um restaurante, gerenciando mesas, requisições, clientes e cardápio.
/// </summary>
class Restaurante
{
    private List<Mesa> mesas;
    private List<ReqMesa> listaRegistros;
    private List<ReqMesa> listaEspera;
    private List<Cliente> clientes;
    private Cardapio cardapio;

    /// <summary>
    /// Inicializa uma nova instância da classe <see cref="Restaurante"/>, criando mesas e listas necessárias.
    /// </summary>
    public Restaurante()
    {
        this.mesas = new List<Mesa>();
        this.listaRegistros = new List<ReqMesa>();
        this.listaEspera = new List<ReqMesa>();
        this.clientes = new List<Cliente>();
        this.cardapio = new Cardapio();

        CriarMesas(4, 4); // 4 mesas de capacidade 4
        CriarMesas(4, 6); // 4 mesas de capacidade 6
        CriarMesas(2, 8); // 2 mesas de capacidade 8
    }

    /// <summary>
    /// Fecha a mesa especificada pelo ID, desocupando-a e retornando o valor da conta.
    /// </summary>
    /// <param name="idMesa">ID da mesa a ser fechada.</param>
    /// <param name="qtdPessoas">Quantidade de pessoas que vão pagar.</param>
    /// <returns>O valor da conta se a mesa foi fechada com sucesso; caso contrário, retorna -1.</returns>
    public double FecharConta(int idMesa, int qtdPessoas)
    {
        ReqMesa? req = listaRegistros.Find(req => req.IdMesa == idMesa); 
        Mesa? mesa = mesas.Find(mesa => mesa.NumeroMesa == idMesa);
        if (mesa != null && req != null)
        {
            mesa.DesocuparMesa();
            req.FecharRequisicao();
            return req.Pedido.FecharConta(qtdPessoas);
        }

        return -1;
    }

    /// <summary>
    /// Processa uma requisição de mesa, alocando uma mesa disponível ou adicionando à lista de espera.
    /// </summary>
    /// <param name="req">A requisição de mesa.</param>
    /// <returns>True se a mesa foi alocada com sucesso; caso contrário, False.</returns>
    public bool ProcessarRequicao(ReqMesa req)
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
    /// Processa a lista de espera, alocando mesas conforme disponibilidade.
    /// </summary>
    /// <returns>String indicando as requisições processadas com sucesso.</returns>
    public string ProcessarListaDeEspera()
    {
        string resposta = "";
        foreach (ReqMesa req in listaEspera)
        {
            foreach (Mesa mesa in mesas)
            {
                if (AlocarMesa(req, mesa))
                {
                    listaEspera.Remove(req);
                    resposta += $"Requisição {req.IdReq} alocada com sucesso.\n";
                }
            }
        }

        return resposta;
    }

    /// <summary>
    /// Adiciona um cliente à lista de clientes.
    /// </summary>
    /// <param name="nome">Nome do cliente.</param>
    /// <returns>O objeto <see cref="Cliente"/> adicionado.</returns>
    public Cliente AdicionarCliente(string nome)
    {
        Cliente cliente = new(nome);
        clientes.Add(cliente);
        return cliente;
    }

    /// <summary>
    /// Cria mesas com a quantidade e capacidade especificadas.
    /// </summary>
    /// <param name="qtdMesas">Quantidade de mesas a serem criadas.</param>
    /// <param name="qtdPessoas">Capacidade de pessoas por mesa.</param>
    private void CriarMesas(int qtdMesas, int qtdPessoas)
    {
        for (int i = 0; i < qtdMesas; i++)
        {
            mesas.Add(new Mesa(i + 1, qtdPessoas));
        }
    }

    /// <summary>
    /// Pede um produto para uma requisição específica.
    /// </summary>
    /// <param name="idProduto">ID do produto.</param>
    /// <param name="idReq">ID da requisição.</param>
    public void PedirProduto(int idProduto, int idReq)
    {
        Produto produto = cardapio.ObterProduto(idProduto);
        ReqMesa? req = listaRegistros.Find(registro => registro.IdReq == idReq);
        if (produto != null && req != null)
        {
            req.AdicionarProduto(produto.Id);
        }
    }

    /// <summary>
    /// Exibe os produtos disponíveis no cardápio.
    /// </summary>
    /// <returns>String com a lista de produtos do cardápio.</returns>
    public string ExibirCardapio()
    {
        return cardapio.MostrarOpcoes();
    }
}
