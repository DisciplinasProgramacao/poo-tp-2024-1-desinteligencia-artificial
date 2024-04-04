internal class FilaEspera
{
  private int capacidadeMesa;
  private Queue<ReqMesa> requisicoes = new Queue<ReqMesa>();

  /// <summary>
  /// Método construtor da classe
  /// </summary>
  /// <param name="capacidadeMesa">Quantidade de pessoas que podem ficar na mesa</param>

  public FilaEspera(int capacidadeMesa)
  {
    this.capacidadeMesa = capacidadeMesa;
  }

  /// <summary>
  ///  Adiciona requisição à fila de requisições
  /// </summary>
  /// <param name="req">ReqMesa para enfileirar</param>

  public void AddRequisicao(ReqMesa req)
  {
    requisicoes.Enqueue(req);
  }

  /// <summary>
  /// Remove requisição da fila e manda uma mensagem de erro se a fila estiver vazia
  /// </summary>
  /// <returns>Requisição removida</returns>
  /// <exception cref="Exception">Se a fila estiver vazia, é enviado "Fila vazia"</exception>

  public ReqMesa RemoverRequisicao()
  {
    if (requisicoes.Count == 0)
    {
      throw new Exception("Fila vazia");
    }

    ReqMesa reqRemovida = requisicoes.Dequeue();
    return reqRemovida;
  }

  /// <summary>
  /// Percorre a fila para ver em qual a posição dos clientes
  /// </summary>

  public void VerFila()
  {
    foreach (ReqMesa r in requisicoes)
    {
      Console.WriteLine(r.nomeCliente);
    }
  }
}