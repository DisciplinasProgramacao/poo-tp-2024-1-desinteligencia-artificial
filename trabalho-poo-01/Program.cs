class Program
{
    static Restaurante restaurante = new Restaurante();

    public static void CadastrarCliente()
    {
        Console.WriteLine("Digite o nome do cliente para cadastro no sistema:");
        string nome = Console.ReadLine();

        Cliente cliente = restaurante.AdicionarCliente(nome);

        if (cliente != null)
        {
            Console.WriteLine("Cliente cadastrado com sucesso!");
        }
        else
        {
            Console.WriteLine("Cliente não foi cadastrado, tente novamente!");
        }
    }

    public static void AtenderCliente()
    {
        Console.WriteLine("Digite o nome do cliente para atendê-lo:");
        string nome = Console.ReadLine();

        Cliente cliente = restaurante.PesquisarCliente(nome);

        if (!cliente)
        {
            Console.WriteLine("Cliente não foi encontrado no sistema, cadastre-o antes de atendê-lo!");
            CadastrarCliente();
            return;
        }

        Console.WriteLine("Digite a quantidade de pessoas que sentarão à mesa:");
        int qntPessoas;

        do
        {
            qntPessoas = int.Parse(Console.ReadLine());

            if (qntPessoas > 0)
            {
                AlocarClienteAMesa(qntPessoas, nome);
            }
            else
            {
                Console.WriteLine("Digite uma quantidade válida!");
            }

        } while (qntPessoas < 1);
    }

    public static void AlocarClienteAMesa(int qntPessoas, string nome)
    {
        ReqMesa req = new ReqMesa(qntPessoas, nome);

        bool alocado = restaurante.ProcessarRequisicao(req);

        if (alocado)
        {
            Console.WriteLine("Cliente foi alocado para uma mesa com sucesso!");
        }
        else
        {
            Console.WriteLine("Não existem mesas disponíveis no momento, cliente movido para fila de espera!");
        }
    }

    static void Main(string[] args)
    {
        int opcao;

        do
        {
            Console.WriteLine("---- MENU RESTAURANTE ----");
            Console.WriteLine("Digite a opção desejada:");
            Console.WriteLine("1) Cadastrar cliente.");
            Console.WriteLine("2) Atender cliente.");
            Console.WriteLine("3) Encerrar o programa.");
            Console.WriteLine("--------------------------");

            opcao = int.Parse(Console.ReadLine());

            switch (opcao)
            {
                case 1:
                    Console.Clear();
                    CadastrarCliente();
                    break;
                case 2:
                    Console.Clear();
                    AtenderCliente();
                    break;
                case 3:
                    Console.Clear();
                    Console.WriteLine("Encerrando programa...");
                    break;
                default:
                    Console.WriteLine("Opção inválida, digite novamente!");
                    break;
            }
        } while (opcao != 3);
    }
}