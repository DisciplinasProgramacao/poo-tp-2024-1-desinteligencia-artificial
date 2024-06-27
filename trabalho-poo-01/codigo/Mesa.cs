/// <summary>
/// Classe que representa uma mesa em um restaurante.
/// </summary>
class Mesa
{

    private static int proximoNumeroMesa = 1;
    // Atributos privados da classe Mesa.
    private int numeroMesa;
    private int capacidadeMaxima;
    private bool estaOcupada;

    /// <summary>
    /// Método que retorna o número da mesa.
    /// </summary>
    public int NumeroMesa
    {
        get => numeroMesa;
    }

    public bool EstaOcupada
    {
        get => estaOcupada;
    }

    /// <summary>
    /// Método construtor da classe Mesa.
    /// </summary>
    /// <param name="capacidadeMaxima">A capacidade máxima de ocupação da mesa.</param>
    public Mesa(int capacidadeMaxima)
    {
        this.numeroMesa = proximoNumeroMesa++;
        this.capacidadeMaxima = capacidadeMaxima;
        estaOcupada = false;
    }

    /// <summary>
    /// Método público para ocupar a mesa.
    /// </summary>
    public void OcuparMesa()
    {
        estaOcupada = true;
    }

    /// <summary>
    /// Método público para desocupar a mesa.
    /// </summary>
    public void DesocuparMesa()
    {
        estaOcupada = false;
    }

    /// <summary>
    /// Método público para verificar a disponibilidade da mesa em relação a uma requisição de X pessoas.
    /// </summary>
    /// <param name="qtdPessoas">Quantidade de pessoas na requisição.</param>
    /// <returns>True se a mesa estiver disponível e a capacidade for suficiente, caso contrário, False.</returns>
    public bool VerificarDisponibilidade(int qtdPessoas)
    {
        return !estaOcupada && capacidadeMaxima >= qtdPessoas;
    }
}