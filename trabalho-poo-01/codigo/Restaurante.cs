using System;
using System.Collections.Generic;

/// <summary>
/// Classe que representa um restaurante.
/// </summary>
class Restaurante
{
    private List<Mesa> mesas;
    private List<ReqMesa> listaRegistros;
    private List<ReqMesa> listaEspera;
    private List<Cliente> clientes;
    private Cardapio cardapio;

    /// <summary>
    /// Construtor da classe Restaurante. Inicializa as listas de mesas, registros, lista de espera, clientes e cardápio.
    /// Cria mesas com diferentes capacidades.
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
    /// Fecha a mesa especificada pelo ID e retorna o valor da conta.
    /// </summary>
    /// <param name="idMesa">ID da mesa a ser fechada.</param>
    /// <param name="qtdPessoas">Quantidade de pessoas que vão pagar.</param>
    /// <returns>O valor da conta se a mesa foi fechada com sucesso, caso contrário, retorna 0.</returns>
    public double FecharConta(int idMesa, int qtdPessoas)
    {
        ReqMesa req = listaRegistros.Find(req => req.idMesa == idMesa); 
        Mesa mesa = mesas.Find(mesa => mesa.numeroMesa == idMesa);
        if (mesa != null)
        {
            mesa.DesocuparMesa();
            return req.FecharRequisicao(qtdPessoas);
        }
        return 0;
    }

    /// <summary>
    /// Aloca uma mesa para a requisição especificada.
    /// </summary>
    /// <param name="req">Requisição de mesa.</param>
    /// <returns>True se a mesa foi alocada com sucesso, caso contrário, False.</returns>
    public bool AlocarMesa(ReqMesa req)
    {
        foreach (Mesa mesa in mesas)
        {
            if (mesa.VerificarDisponibilidade(req.qtdPessoas))
            {
                mesa.OcuparMesa();
                req.AtribuirMesaARequisicao(mesa.numeroMesa);
                listaRegistros.Add(req);
                return true;
            }
        }

        listaEspera.Add(req);
        return false;
    }

    /// <summary>
    /// Processa a lista de espera para alocar mesas.
    /// </summary>
    /// <returns>True se pelo menos uma requisição foi processada com sucesso, caso contrário, False.</returns>
    public bool ProcessarListaDeEspera()
    {
        foreach (ReqMesa req in listaEspera)
        {
            if (AlocarMesa(req))
            {
                listaEspera.Remove(req);
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// Adiciona um cliente à lista de clientes.
    /// </summary>
    /// <param name="nome">Nome do cliente.</param>
    /// <returns>O objeto Cliente adicionado.</returns>
    public Cliente AdicionarCliente(string nome)
    {
        Cliente cliente = new Cliente{Nome = nome};
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
            mesas.Add(new Mesa { numeroMesa = i + 1, capacidade = qtdPessoas, isOcupada = false });
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
        ReqMesa req = listaRegistros.Find(registro => registro.idReq == idReq);
        if (produto != null && req != null)
        {
            req.ReceberProduto(produto.id);
        }
    }

    /// <summary>
    /// Chama o método de mostrar produtos do cardápio
    /// </summary>
    /// <returns>Lista de produtos do cardápio</returns>
    public string ExibirCardapio(){
        return cardapio.MostrarOpcoes();
    }
}
