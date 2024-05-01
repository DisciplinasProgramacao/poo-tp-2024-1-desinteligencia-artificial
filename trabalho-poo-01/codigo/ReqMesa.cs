public enum StatusRequisicao
{
    EmEspera = 0,
    Atendendo = 1,
    Finalizada = 2
}

/// <summary>
/// Classe que armazena as requisi��es de uma mesa em um restaurante.
/// </summary>
class ReqMesa {
    private int idReq;
    private int qtdPessoas;
    private int? idMesa;
    private string nomeCliente;
    private DateTime dataEntrada;
    private DateTime dataSaida;
    private StatusRequisicao status;

    /// <summary>
    /// M�todo construtor de uma requisi��o onde possui-se uma mesa alocada
    /// </summary>
    /// <param name="qtdPessoas">Quantidade de capacidade de pessoas na mesa</param>
    /// <param name="nomeCliente">Nome do cliente</param>
    /// <param name="idMesa">C�digo ID da mesa que deseja ser atribuida</param>
    public ReqMesa(int qtdPessoas, string nomeCliente, int idMesa)
    {
        Random random = new Random();
        this.idReq = random.Next(99999);
        this.qtdPessoas = qtdPessoas;
        this.nomeCliente = nomeCliente;
        this.idMesa = idMesa;
        this.dataEntrada = DateTime.Now;
        this.status = StatusRequisicao.EmEspera;
    }

    /// <summary>
    /// M�todo construtor de uma requisi��o onde n�o possui-se uma mesa, feita para inserir na FilaDeEspera
    /// </summary>
    /// <param name="qtdPessoas">Quantidade de capacidade de pessoas na mesa</param>
    /// <param name="nomeCliente">Nome do cliente</param>
    public ReqMesa(int qtdPessoas, string nomeCliente)
    {
        Random random = new Random();
        this.idReq = random.Next(99999);
        this.qtdPessoas = qtdPessoas;
        this.nomeCliente = nomeCliente;
        this.dataEntrada = DateTime.Now;
        this.status = StatusRequisicao.EmEspera;
    }

    /// <summary>
    /// M�todo para fechar a requisi��o aberta, consiste em definir a data de sa�da para a requisi��o. Tamb�m, altera o status da requisi��o para finalizada
    public void FecharRequisicao() {
    /// </summary>
        this.dataSaida = DateTime.Now;
        this.status = StatusRequisicao.Finalizada;
    }

    /// <summary>
    /// M�todo para atribuir uma mesa a requisi��o e alterar o status da requisi��o para atendendo
    /// </summary>
    /// <param name="idMesa">C�digo ID da mesa que deseja ser atribuida</param>
    public void AtribuirMesaARequisicao(int idMesa)
    {
        this.idMesa = idMesa;
        this.status = StatusRequisicao.Atendendo;
    }
}