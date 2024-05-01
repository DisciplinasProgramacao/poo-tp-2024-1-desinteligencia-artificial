public enum StatusRequisicao
{
    EmEspera = 0,
    Atendendo = 1,
    Finalizada = 2
}

/// <summary>
/// Classe que armazena as requisições de uma mesa em um restaurante.
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
    /// Método construtor de uma requisição onde possui-se uma mesa alocada
    /// </summary>
    /// <param name="qtdPessoas">Quantidade de capacidade de pessoas na mesa</param>
    /// <param name="nomeCliente">Nome do cliente</param>
    /// <param name="idMesa">Código ID da mesa que deseja ser atribuida</param>
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
    /// Método construtor de uma requisição onde não possui-se uma mesa, feita para inserir na FilaDeEspera
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
    /// Método para fechar a requisição aberta, consiste em definir a data de saída para a requisição. Também, altera o status da requisição para finalizada
    public void FecharRequisicao() {
    /// </summary>
        this.dataSaida = DateTime.Now;
        this.status = StatusRequisicao.Finalizada;
    }

    /// <summary>
    /// Método para atribuir uma mesa a requisição e alterar o status da requisição para atendendo
    /// </summary>
    /// <param name="idMesa">Código ID da mesa que deseja ser atribuida</param>
    public void AtribuirMesaARequisicao(int idMesa)
    {
        this.idMesa = idMesa;
        this.status = StatusRequisicao.Atendendo;
    }
}