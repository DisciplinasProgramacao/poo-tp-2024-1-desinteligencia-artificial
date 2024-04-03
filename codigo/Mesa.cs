/// <summary>
/// Classe que representa uma mesa em um restaurante.
/// </summary>
class Mesa
{
    // Atributos privados da classe Mesa
    private int numeroMesa;
    private int capacidadeMaxima;
    private bool estaOcupada;

    /// <summary>
    /// Método construtor da classe Mesa.
    /// </summary>
    /// <param name="numeroMesa">O número identificador da mesa.</param>
    /// <param name="capacidadeMaxima">A capacidade máxima de ocupação da mesa.</param>
    public Mesa(int numeroMesa, int capacidadeMaxima)
    {
        this.numeroMesa = numeroMesa;
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

}