class Cliente
{

    public string Nome { get; set; }

    private Restaurante restaurante;

    /// <summary>
    /// Inicializa uma nova instância da classe Cliente com o restaurante fornecido.
    /// </summary>
    /// <param name="restaurante">O restaurante no qual o cliente deseja fazer a reserva.</param>

    public Cliente(Restaurante restaurante)
    {
        this.restaurante = restaurante;
    }

    /// <summary>
    /// Tenta fazer uma reserva de mesa para o cliente no restaurante.
    /// </summary>
    /// <param name="qtdPessoas">O número de pessoas para as quais a mesa será reservada.</param>
    /// <param name="nome">O nome do cliente.</param>
    /// <returns>Um valor booleano que indica se a mesa foi reservada com sucesso.</returns>
    public bool PedirMesa(int qtdPessoas, string nome)
    {

        bool mesaAlocada = restaurante.AlocarMesa(qtdPessoas, nome);
        if (mesaAlocada)
        {
            Console.WriteLine($"Mesa alocada para {nome} para {qtdPessoas} pessoas.");
            return true;
        }
        else
        {
            Console.WriteLine($"Não foi possível alocar uma mesa para {nome}.");
            return false;
        }
    }

}
