using System;
using System.Collections.Generic;

/// <summary>
/// Classe principal do programa que gerencia o fluxo do Restaurante e do Café.
/// </summary>
class Program
{
    static Restaurante restaurante = new Restaurante();
    static Cafe cafe = new Cafe();

    /// <summary>
    /// Método para cadastrar um novo cliente no sistema.
    /// </summary>
    public static void CadastrarCliente()
    {
        Console.WriteLine("Digite o nome do cliente para cadastro no sistema:");
        string nome = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(nome))
        {
            Console.WriteLine("Nome inválido! O nome do cliente não pode ser vazio.");
            CadastrarCliente();
        }

        Cliente cliente = restaurante.AdicionarCliente(nome);

        if (cliente != null)
        {
            Console.WriteLine("Cliente cadastrado com sucesso!");
        }
        else
        {
            Console.WriteLine("Cliente já cadastrado! Tente novamente!");
            CadastrarCliente();
        }
    }

    /// <summary>
    /// Método para atender um cliente, verificando se está cadastrado e alocando uma mesa.
    /// </summary>
    public static void AtenderCliente()
    {
        Console.WriteLine("Digite o nome do cliente para atendê-lo:");
        string nome = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(nome))
        {
            Console.WriteLine("Nome inválido! O nome do cliente não pode ser vazio.");
            AtenderCliente();
            return;
        }

        Cliente? cliente = restaurante.PesquisarCliente(nome);

        if (cliente == null)
        {
            Console.WriteLine("Cliente não foi encontrado no sistema, cadastre-o antes de atendê-lo!");
            CadastrarCliente();
            return;
        }

        Console.WriteLine("Digite a quantidade de pessoas que sentarão à mesa:");

        int qntPessoas;

        while (!int.TryParse(Console.ReadLine(), out qntPessoas) || qntPessoas < 1 || qntPessoas > 8)
        {
            if (qntPessoas > 8)
                Console.WriteLine("Não existem mesas com mais de 8 lugares!");

            Console.WriteLine("Digite uma quantidade válida!");
        }

        AlocarClienteAMesa(qntPessoas, nome);
    }

    /// <summary>
    /// Método para alocar uma mesa para o cliente.
    /// </summary>
    /// <param name="qntPessoas">Quantidade de pessoas que vão sentar à mesa.</param>
    /// <param name="nome">Nome do cliente.</param>
    private static void AlocarClienteAMesa(int qntPessoas, string nome)
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

    /// <summary>
    /// Método para exibir o cardápio do restaurante.
    /// </summary>
    /// <param name="restaurante">Instância do restaurante.</param>
    public static void MostrarCardapio(Restaurante restaurante)
    {
        Console.WriteLine(restaurante.ExibirCardapio());
    }

    /// <summary>
    /// Método para exibir o cardápio do café.
    /// </summary>
    /// <param name="cafe">Instância do café.</param>
    public static void MostrarCardapio(Cafe cafe)
    {
        Console.WriteLine(cafe.ExibirCardapio());
    }

    /// <summary>
    /// Método para listar a fila de espera do restaurante.
    /// </summary>
    public static void ListarFilaDeEspera()
    {
        Console.WriteLine(restaurante.ListarFilaDeEspera());
    }

    /// <summary>
    /// Método para anotar o pedido de uma mesa.
    /// </summary>
    public static void AnotarPedidoMesa()
    {
        Console.WriteLine("Digite o número da mesa:");
        int numeroMesa = int.Parse(Console.ReadLine());

        Mesa? mesa = restaurante.PesquisarMesa(numeroMesa);

        if (mesa == null)
        {
            Console.WriteLine("Mesa não encontrada.");
            return;
        }

        Console.WriteLine("Digite o código do produto:");
        int codigoProduto = int.Parse(Console.ReadLine());
        Produto? produto = restaurante.PesquisarProduto(codigoProduto);

        if (produto == null)
        {
            Console.WriteLine("Produto não encontrado.");
            return;
        }

        Console.WriteLine("Digite a quantidade:");
        int quantidade = int.Parse(Console.ReadLine());

        ReqMesa? req = restaurante.ObterRequisicaoPorMesa(numeroMesa);

        if (req == null)
        {
            Console.WriteLine("Mesa não encontrada.");
            return;
        }

        restaurante.PedirProduto(codigoProduto, quantidade, req.IdReq);
        Console.WriteLine("Pedido anotado com sucesso.");
    }

    /// <summary>
    /// Método para adicionar uma nova mesa ao restaurante.
    /// </summary>
    public static void AdicionarMesa()
    {
        Console.WriteLine("Digite a capacidade máxima da mesa:");
        int capacidade = int.Parse(Console.ReadLine());
        restaurante.CriarMesa(capacidade);
        Console.WriteLine("Mesa adicionada com sucesso.");
    }

    /// <summary>
    /// Método para adicionar um novo produto ao cardápio do restaurante.
    /// </summary>
    public static void AdicionarProduto()
    {
        Console.WriteLine("Digite o código do novo produto:");
        int codigo = int.Parse(Console.ReadLine());
        Console.WriteLine("Digite o nome do produto:");
        string nome = Console.ReadLine();
        Console.WriteLine("Digite a descrição do produto:");
        string descricao = Console.ReadLine();
        Console.WriteLine("Digite o valor do produto:");
        double valor = double.Parse(Console.ReadLine());

        Produto produto = new Produto(codigo, nome, valor, descricao);
        restaurante.AdicionarProdutoAoCardapio(produto);
        Console.WriteLine("Produto adicionado com sucesso.");
    }

    /// <summary>
    /// Método para fechar a conta de uma mesa.
    /// </summary>
    public static void FecharConta()
    {
        Console.WriteLine("Digite o número da mesa:");

        int numeroMesa;
        while (!int.TryParse(Console.ReadLine(), out numeroMesa))
        {
            Console.WriteLine("Digite um número de mesa válido!");
        }

        string resposta = restaurante.FecharConta(numeroMesa);
        Console.WriteLine(resposta);
    }

    /// <summary>
    /// Método para exibir o menu do café e gerenciar as opções.
    /// </summary>
    static void MenuCafe()
    {
        Console.Clear();

        int opcao;

        do
        {
            Console.WriteLine();
            Console.WriteLine("---- MENU CAFÉ ----");
            Console.WriteLine("Digite a opção desejada:");
            Console.WriteLine("1) Ver o cardápio");
            Console.WriteLine("2) Voltar ao menu principal");
            Console.WriteLine("--------------------------");

            bool opcaoValida = int.TryParse(Console.ReadLine(), out opcao);

            if (!opcaoValida)
            {
                Console.WriteLine("Opção inválida, digite novamente!");
                continue;
            }

            switch (opcao)
            {
                case 1:
                    Console.Clear();
                    MostrarCardapio(cafe);
                    break;
                case 2:
                    Console.Clear();
                    return;
                default:
                    Console.WriteLine("Opção inválida, digite novamente!");
                    break;
            }
        } while (opcao != 2);
    }

    /// <summary>
    /// Método para exibir o menu do restaurante e gerenciar as opções.
    /// </summary>
    static void MenuRestaurante()
    {
        Console.Clear();

        int opcao;

        do
        {
            Console.WriteLine();
            Console.WriteLine("---- MENU RESTAURANTE ----");
            Console.WriteLine("Digite a opção desejada:");
            Console.WriteLine("1) Cadastrar cliente.");
            Console.WriteLine("2) Atender cliente.");
            Console.WriteLine("3) Listar fila de espera.");
            Console.WriteLine("4) Anotar pedido da mesa.");
            Console.WriteLine("5) Adicionar mesa.");
            Console.WriteLine("6) Adicionar produto.");
            Console.WriteLine("7) Ver o cardápio");
            Console.WriteLine("8) Fechar conta");
            Console.WriteLine("9) Voltar ao menu principal");
            Console.WriteLine("--------------------------");

            bool opcaoValida = int.TryParse(Console.ReadLine(), out opcao);

            if (!opcaoValida)
            {
                Console.WriteLine("Opção inválida, digite novamente!");
                continue;
            }

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
                    ListarFilaDeEspera();
                    break;
                case 4:
                    Console.Clear();
                    AnotarPedidoMesa();
                    break;
                case 5:
                    Console.Clear();
                    AdicionarMesa();
                    break;
                case 6:
                    Console.Clear();
                    AdicionarProduto();
                    break;
                case 7:
                    Console.Clear();
                    MostrarCardapio(restaurante);
                    break;
                case 8:
                    Console.Clear();
                    FecharConta();
                    break;
                case 9:
                    Console.Clear();
                    return;
                default:
                    Console.WriteLine("Opção inválida, digite novamente!");
                    break;
            }
        } while (opcao != 9);
    }

    /// <summary>
    /// Método principal do programa, exibe o menu principal e gerencia a navegação.
    /// </summary>
    static void Main()
    {
        int opcao;

        do
        {
            Console.WriteLine("---- MENU PRINCIPAL ----");
            Console.WriteLine("Digite a opção desejada:");
            Console.WriteLine("1) Menu Restaurante");
            Console.WriteLine("2) Menu Café");
            Console.WriteLine("3) Encerrar o programa");
            Console.WriteLine("--------------------------");

            bool opcaoValida = int.TryParse(Console.ReadLine(), out opcao);

            if (!opcaoValida)
            {
                Console.WriteLine("Opção inválida, digite novamente!");
                continue;
            }

            switch (opcao)
            {
                case 1:
                    MenuRestaurante();
                    break;
                case 2:
                    MenuCafe();
                    break;
                case 3:
                    Console.WriteLine("Encerrando programa...");
                    break;
                default:
                    Console.WriteLine("Opção inválida, digite novamente!");
                    break;
            }
        } while (opcao != 3);
    }
}
