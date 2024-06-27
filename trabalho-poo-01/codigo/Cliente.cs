class Cliente
{

    private string nome;

    /// <summary>
    /// Inicializa uma nova instância da classe Cliente com o nome fornecido.
    /// </summary>
    public Cliente(string nome)
    {
        this.nome = nome;
    }

    /// <summary>
    /// Método que retorna o nome do cliente.
    /// </summary>
    public string Nome
    {
        get => nome;
    }

}