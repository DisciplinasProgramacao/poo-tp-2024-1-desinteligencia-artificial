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
    public static void CadastrarCliente(Loja loja)
    {
        Console.WriteLine("Digite o nome do cliente para cadastro no sistema:");
        string nome = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(nome))
        {
            Console.WriteLine("Nome inválido! O nome do cliente não pode ser vazio.");
            CadastrarCliente(loja);
        }

        Cliente cliente = loja.AdicionarCliente(nome);

        if (cliente != null)
        {
            Console.WriteLine("Cliente cadastrado com sucesso!");
        }
        else
        {
            Console.WriteLine("Cliente já cadastrado! Tente novamente!");
            CadastrarCliente(loja);
        }
    }

    /// <summary>
    /// Método para atender um cliente, verificando se está cadastrado e alocando uma mesa.
    /// </summary>
    public static void AtenderCliente(Loja loja)
    {
        Console.WriteLine("Digite o nome do cliente para atendê-lo:");
        string nome = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(nome))
        {
            Console.WriteLine("Nome inválido! O nome do cliente não pode ser vazio.");
            AtenderCliente(loja);
            return;
        }

        Cliente? cliente = loja.PesquisarCliente(nome);

        if (cliente == null)
        {
            Console.WriteLine("Cliente não foi encontrado no sistema, cadastre-o antes de atendê-lo!");
            CadastrarCliente(loja);
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
    /// Método para exibir o cardápio da loja.
    /// </summary>
    /// <param name="loja">Instância de loja.</param>
    public static void MostrarCardapio(Loja loja)
    {
        Console.WriteLine(loja.ExibirCardapio());
    }

    /// <summary>
    /// Método para listar a fila de espera do restaurante.
    /// </summary>
    public static void ListarFilaDeEspera()
    {
        Console.WriteLine(restaurante.ListarFilaDeEspera());
    }

    /// <summary>
    /// Método para listar as mesas do restaurante.
    /// </summary>
    public static void ListarMesas(Loja loja)
    {
        Console.WriteLine(loja.ListarMesas());
    }

    /// <summary>
    /// Método para anotar o pedido de uma mesa.
    /// </summary>
    public static void AnotarPedidoMesa(Loja loja)
    {
        Console.WriteLine("Digite o número da mesa:");
        int numeroMesa = int.Parse(Console.ReadLine());

        Mesa? mesa = loja.PesquisarMesa(numeroMesa);

        if (mesa == null)
        {
            Console.WriteLine("Mesa não encontrada.");
            return;
        }

        // TODO: MOSTRAR CARDÁPIO
        Console.WriteLine("Digite o código do produto:");
        int codigoProduto = int.Parse(Console.ReadLine());
        Produto? produto = loja.PesquisarProduto(codigoProduto);

        if (produto == null)
        {
            Console.WriteLine("Produto não encontrado.");
            return;
        }

        Console.WriteLine("Digite a quantidade:");
        int quantidade = int.Parse(Console.ReadLine());

        ReqMesa? req = loja.ObterRequisicaoPorMesa(numeroMesa);

        if (req == null)
        {
            Console.WriteLine("Mesa não encontrada.");
            return;
        }

        string resposta = loja.PedirProduto(codigoProduto, quantidade, req.IdReq);
        Console.WriteLine(resposta);
    }

    /// <summary>
    /// Método para adicionar uma nova mesa ao restaurante.
    /// </summary>
    public static void AdicionarMesa(Loja loja)
    {
        Console.WriteLine("Digite a capacidade máxima da mesa:");
        int capacidade = int.Parse(Console.ReadLine());
        loja.CriarMesa(capacidade);
        Console.WriteLine("Mesa adicionada com sucesso.");
    }

    /// <summary>
    /// Método para adicionar um novo produto ao cardápio do restaurante.
    /// </summary>
    public static void AdicionarProduto(Loja loja)
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
        loja.AdicionarProdutoAoCardapio(produto);
        Console.WriteLine("Produto adicionado com sucesso.");
    }

    /// <summary>
    /// Método para fechar a conta de uma mesa.
    /// </summary>
    public static void FecharConta(Loja loja)
    {
        Console.WriteLine("Digite o número da mesa:");

        int numeroMesa;
        while (!int.TryParse(Console.ReadLine(), out numeroMesa))
        {
            Console.WriteLine("Digite um número de mesa válido!");
        }

        string resposta = loja.FecharConta(numeroMesa);
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
            Console.WriteLine("4) Ver mesas.");
            Console.WriteLine("5) Anotar pedido da mesa.");
            Console.WriteLine("6) Adicionar mesa.");
            Console.WriteLine("7) Adicionar produto.");
            Console.WriteLine("8) Ver o cardápio");
            Console.WriteLine("9) Fechar conta");
            Console.WriteLine("10) Voltar ao menu principal");
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
                    CadastrarCliente(restaurante);
                    break;
                case 2:
                    Console.Clear();
                    AtenderCliente(restaurante);
                    break;
                case 3:
                    Console.Clear();
                    ListarFilaDeEspera();
                    break;
                case 4:
                    Console.Clear();
                    ListarMesas(restaurante);
                    break;
                case 5:
                    Console.Clear();
                    AnotarPedidoMesa(restaurante);
                    break;
                case 6:
                    Console.Clear();
                    AdicionarMesa(restaurante);
                    break;
                case 7:
                    Console.Clear();
                    AdicionarProduto(restaurante);
                    break;
                case 8:
                    Console.Clear();
                    MostrarCardapio(restaurante);
                    break;
                case 9:
                    Console.Clear();
                    FecharConta(restaurante);
                    break;
                case 10:
                    Console.Clear();
                    return;
                default:
                    Console.WriteLine("Opção inválida, digite novamente!");
                    break;
            }
        } while (opcao != 10);
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
