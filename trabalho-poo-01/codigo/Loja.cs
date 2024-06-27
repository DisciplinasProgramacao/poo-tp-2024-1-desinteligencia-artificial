using System;
using System.Collections.Generic;

abstract class Loja
{
    protected List<Mesa> mesas;
    protected List<ReqMesa> listaRegistros;
    protected List<Cliente> clientes;
    protected Cardapio cardapio;

    /// <summary>
    /// Inicializa uma nova instância da classe <see cref="Restaurante"/>, criando mesas e listas necessárias.
    /// </summary>
    public Loja()
    {
        this.mesas = new List<Mesa>();
        this.listaRegistros = new List<ReqMesa>();
        this.clientes = new List<Cliente>();
        this.cardapio = CriarCardapio();


        int[] quantidades = { 4, 4, 2 };
        int[] capacidades = { 4, 6, 8 };

        for (int i = 0; i < quantidades[i]; i++)
        {
            CriarMesa(capacidades[i]);
        }

    }

    /// <summary>
    /// Fecha a mesa especificada pelo ID, desocupando-a e retornando o valor da conta.
    /// </summary>
    /// <param name="idMesa">ID da mesa a ser fechada.</param>
    /// <returns>O valor da conta se a mesa foi fechada com sucesso; caso contrário, retorna -1.</returns>
    public double FecharConta(int idMesa)
    {
        ReqMesa? req = listaRegistros.Find(req => req.IdMesa == idMesa);
        Mesa? mesa = mesas.Find(mesa => mesa.NumeroMesa == idMesa);
        if (mesa != null && req != null)
        {
            mesa.DesocuparMesa();
            return req.FecharRequisicao();
        }

        return -1;
    }

    /// <summary>
    /// Processa uma requisição de mesa, alocando uma mesa disponível ou adicionando à lista de espera.
    /// </summary>
    /// <param name="req">A requisição de mesa.</param>
    /// <returns>True se a mesa foi alocada com sucesso; caso contrário, False.</returns>
    abstract public bool ProcessarRequisicao(ReqMesa req);

    /// <summary>
    /// Aloca uma mesa específica para uma requisição se disponível.
    /// </summary>
    /// <param name="req">A requisição de mesa.</param>
    /// <param name="mesa">A mesa a ser alocada.</param>
    /// <returns>True se a mesa foi alocada com sucesso; caso contrário, False.</returns>
    abstract protected bool AlocarMesa(ReqMesa req, Mesa mesa);

    abstract protected Cardapio CriarCardapio();

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
    public void CriarMesa(int qtdPessoas)
    {
        mesas.Add(new Mesa(qtdPessoas));
    }

    /// <summary>
    /// Pede um produto para uma requisição específica.
    /// </summary>
    /// <param name="idProduto">ID do produto.</param>
    /// <param name="quantidade">Quantidade do produto.</param>
    /// <param name="idReq">ID da requisição.</param>
    public void PedirProduto(int idProduto, int quantidade, int idReq)
    {
        Produto? produto = cardapio.ObterProduto(idProduto);
        ReqMesa? req = listaRegistros.Find(registro => registro.IdReq == idReq);
        if (produto != null && req != null)
        {
            req.ReceberProdutos(produto, quantidade);
        }
    }

    /// <summary>
    /// Exibe os produtos disponíveis no cardápio.
    /// </summary>
    /// <returns>String com a lista de produtos do cardápio.</returns>
    public string ExibirCardapio()
    {
        return cardapio.MostrarCardapio();
    }

    /// <summary>
    /// Pesquisa um cliente pelo nome.
    /// </summary>
    /// <param name="nome">Nome a ser pesquisado.</param>
    /// <returns>Objeto cliente ou nulo se não existir na lista.</returns>
    public Cliente? PesquisarCliente(string nome)
    {
        Cliente? cliente = clientes.Find(cliente => cliente.Nome == nome);
        return cliente;
    }

    /// <summary>
    /// Pesquisa uma mesa pelo número.
    /// </summary>
    /// <param name="idMesa">Número da mesa a ser pesquisada.</param>
    /// <returns>Objeto mesa ou nulo se não existir na lista.</returns>
    public Mesa? PesquisarMesa(int idMesa)
    {
        Mesa? mesa = mesas.Find(mesa => mesa.NumeroMesa == idMesa);
        return mesa;
    }

    /// <summary>
    /// Pesquisa um produto pelo ID.
    /// </summary>
    /// <param name="idProduto">ID do produto a ser pesquisado.</param>
    /// <returns>Objeto produto ou nulo se não existir no cardápio.</returns>
    public Produto? PesquisarProduto(int idProduto)
    {
        Produto? produto = cardapio.ObterProduto(idProduto);
        return produto;
    }

    public ReqMesa? ObterRequisicaoPorMesa(int idMesa)
    {
        ReqMesa? req = listaRegistros.Find(req => req.IdMesa == idMesa);
        return req;
    }

    public void AdicionarProdutoAoCardapio(Produto produto)
    {
        cardapio.AdicionarProduto(produto);
    }

}
