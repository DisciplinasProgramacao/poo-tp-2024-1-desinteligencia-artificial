class Program
{
    public static Cliente CadastrarCliente()
    {
        Console.WriteLine("Digite o nome do cliente para cadastro no sistema:");
        string nome = Console.ReadLine();
        return new Cliente(nome);
    }

    public static void AtenderCliente(Cliente cliente)
    {
        Console.WriteLine("Digite a quantidade de pessoas que sentarão à mesa:");
        int qntPessoas;

        do
        {
            qntPessoas = int.Parse(Console.ReadLine());

            if (qntPessoas > 0)
            {
                cliente.PedirMesa(qntPessoas, cliente.Nome);
            }
            else
            {
                Console.WriteLine("Digite uma quantidade válida!");
            }

        } while (qntPessoas < 1);
    }

    static void Main(string[] args)
    {
        int opcao;

        do
        {
            Console.WriteLine("---- MENU RESTAURANTE ----");
            Console.WriteLine("Digite a opção desejada:");
            Console.WriteLine("1) Atender cliente.");
            Console.WriteLine("2) Encerrar o programa.");
            Console.WriteLine("--------------------------");

            opcao = int.Parse(Console.ReadLine());

            switch (opcao)
            {
                case 1:
                    Console.Clear();
                    Cliente cliente = CadastrarCliente();
                    AtenderCliente(cliente);
                    break;
                case 2:
                    Console.Clear();
                    Console.WriteLine("Encerrando programa...");
                    break;
                default:
                    Console.WriteLine("Opção inválida, digite novamente!");
                    break;
            }
        } while (opcao != 2);
    }
}