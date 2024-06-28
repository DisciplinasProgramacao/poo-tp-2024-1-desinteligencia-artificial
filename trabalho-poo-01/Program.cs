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

        Cliente? cliente = loja.AdicionarCliente(nome);

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


    public static void AtenderClienteCafe()
    {
        Console.WriteLine("Digite o nome do cliente para ser atendido:");
        string nome = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(nome))
        {
            Console.WriteLine("Nome inválido! O nome do cliente não pode ser vazio.");
            return;
        }

        Cliente? cliente = cafe.PesquisarCliente(nome);

        if (cliente == null)
        {
            Console.WriteLine("Cliente não existe no sistema!");
            return;
        }


        ReqMesa req;
        Console.WriteLine("O cliente deseja sentar-se em uma mesa? (S/N)");
        string resposta = Console.ReadLine();

        if (resposta.ToUpper() == "S")
        {
            ListarMesas(cafe);
            Console.WriteLine("Qual mesa o cliente deseja sentar:");
            int numMesa;

            while (!int.TryParse(Console.ReadLine(), out numMesa))
            {
                Console.WriteLine("Digite um número de mesa válido:");
            }

            Mesa? mesa = cafe.PesquisarMesa(numMesa);

            if (mesa == null)
            {
                Console.WriteLine("Mesa não encontrada!");
                return;
            }
            else if (mesa.EstaOcupada)
            {
                Console.WriteLine("Mesa ocupada!");
                return;
            }

            req = new ReqMesa(1, nome, numMesa);
        }
        else
        {
            req = new ReqMesa(1, nome);
        }

        cafe.AdicionarRequisicao(req);
        Console.WriteLine("Cliente atendido!");
    }

    /// <summary>
    /// Método para atender um cliente, verificando se está cadastrado e alocando uma mesa.
    /// </summary>
    public static void AtenderClienteRestaurante()
    {
        Console.WriteLine("Digite o nome do cliente para atendê-lo:");
        string nome = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(nome))
        {
            Console.WriteLine("Nome inválido! O nome do cliente não pode ser vazio.");
            AtenderClienteRestaurante();
            return;
        }

        Cliente? cliente = restaurante.PesquisarCliente(nome);

        if (cliente == null)
        {
            Console.WriteLine("Cliente não foi encontrado no sistema, cadastre-o antes de atendê-lo!");
            CadastrarCliente(restaurante);
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
    /// Método para atender a fila de espera do restaurante.
    /// </summary>
    public static void AtenderFilaDeEspera()
    {
        Console.WriteLine(restaurante.ProcessarListaDeEspera());
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
    public static void AnotarPedidoRestaurante()
    {
        ListarMesas(restaurante);
        Console.WriteLine("Digite o número da mesa:");
        int numeroMesa = int.Parse(Console.ReadLine());

        Mesa? mesa = restaurante.PesquisarMesa(numeroMesa);

        if (mesa == null)
        {
            Console.WriteLine("Mesa não encontrada.");
            return;
        }

        MostrarCardapio(restaurante);
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

        string resposta = restaurante.PedirProduto(codigoProduto, quantidade, req);
        Console.WriteLine(resposta);
    }

    /// <summary>
    /// Método para anotar o pedido de uma mesa no café.
    /// </summary>
    public static void AnotarPedidoCafe()
    {
        Console.WriteLine("Digite o nome do cliente:");
        string nomeCliente = Console.ReadLine();

        ReqMesa? req = cafe.ObterRequisicaoPorCliente(nomeCliente);
        if (req == null)
        {
            Console.WriteLine("Pedido do cliente não encontrado.");
            return;
        }

        MostrarCardapio(cafe);
        Console.WriteLine("Digite o código do produto:");
        int codigoProduto = int.Parse(Console.ReadLine());
        Produto? produto = cafe.PesquisarProduto(codigoProduto);

        if (produto == null)
        {
            Console.WriteLine("Produto não encontrado.");
            return;
        }

        Console.WriteLine("Digite a quantidade:");
        int quantidade = int.Parse(Console.ReadLine());

        string resposta = cafe.PedirProduto(codigoProduto, quantidade, req);
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
    public static void FecharContaRestaurante()
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

    public static void FecharPedidoCafe()
    {
        Console.WriteLine("Digite o nome do cliente:");
        string nomeCliente = Console.ReadLine();

        string resposta = cafe.FecharConta(nomeCliente);
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
            Console.WriteLine("1) Cadastrar cliente.");
            Console.WriteLine("2) Atender cliente.");
            Console.WriteLine("3) Ver mesas.");
            Console.WriteLine("4) Anotar pedido do cliente.");
            Console.WriteLine("5) Adicionar mesa.");
            Console.WriteLine("6) Adicionar produto.");
            Console.WriteLine("7) Ver o cardápio");
            Console.WriteLine("8) Fechar pedido");
            Console.WriteLine("10) Ver clientes");
            Console.WriteLine("11) Ver requisições");
            Console.WriteLine("12) Voltar ao menu principal");
            Console.WriteLine("--------------------------");

            bool opcaoValida = int.TryParse(Console.ReadLine(), out opcao);

            if (!opcaoValida)
            {
                Console.WriteLine("Opção inválida, digite novamente!");
                continue;
            }

            Console.WriteLine();

            switch (opcao)
            {
                case 1:
                    CadastrarCliente(cafe);
                    break;
                case 2:
                    AtenderClienteCafe();
                    break;
                case 3:
                    ListarMesas(cafe);
                    break;
                case 4:
                    AnotarPedidoCafe();
                    break;
                case 5:
                    AdicionarMesa(cafe);
                    break;
                case 6:
                    AdicionarProduto(cafe);
                    break;
                case 7:
                    MostrarCardapio(cafe);
                    break;
                case 8:
                    FecharPedidoCafe();
                    break;
                case 10:
                    Console.WriteLine(cafe.ListarClientes());
                    break;
                case 11:
                    Console.WriteLine(cafe.listaRegistrosAtendimentos());
                    break;
                case 12:
                    return;
                default:
                    Console.WriteLine("Opção inválida, digite novamente!");
                    break;
            }
        } while (opcao != 12);
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
            Console.WriteLine("4) Atender fila de espera.");
            Console.WriteLine("5) Ver mesas.");
            Console.WriteLine("6) Anotar pedido da mesa.");
            Console.WriteLine("7) Adicionar mesa.");
            Console.WriteLine("8) Adicionar produto.");
            Console.WriteLine("9) Ver o cardápio");
            Console.WriteLine("10) Ver clientes");
            Console.WriteLine("11) Ver requisições");
            Console.WriteLine("12) Fechar conta");
            Console.WriteLine("13) Voltar ao menu principal");
            Console.WriteLine("--------------------------");

            bool opcaoValida = int.TryParse(Console.ReadLine(), out opcao);

            if (!opcaoValida)
            {
                Console.WriteLine("Opção inválida, digite novamente!");
                continue;
            }

            Console.WriteLine();

            switch (opcao)
            {
                case 1:
                    CadastrarCliente(restaurante);
                    break;
                case 2:
                    AtenderClienteRestaurante();
                    break;
                case 3:
                    ListarFilaDeEspera();
                    break;
                case 4:
                    AtenderFilaDeEspera();
                    break;
                case 5:
                    ListarMesas(restaurante);
                    break;
                case 6:
                    AnotarPedidoRestaurante();
                    break;
                case 7:
                    AdicionarMesa(restaurante);
                    break;
                case 8:
                    AdicionarProduto(restaurante);
                    break;
                case 9:
                    MostrarCardapio(restaurante);
                    break;
                case 10:
                    Console.WriteLine(restaurante.ListarClientes());
                    break;
                case 11:
                    Console.WriteLine(restaurante.listaRegistrosAtendimentos());
                    break;
                case 12:
                    FecharContaRestaurante();
                    break;
                case 13:
                    return;
                default:
                    Console.WriteLine("Opção inválida, digite novamente!");
                    break;
            }
        } while (opcao != 13);
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
