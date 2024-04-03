class Cliente
{
    #region Atributos 
    public string Nome { get; set; }

    private Restaurante restaurante;

    #endregion


    public Cliente(Restaurante restaurante)
    {
        this.restaurante = restaurante;
    }

    
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