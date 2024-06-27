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

        Cliente cliente = restaurante.AdicionarCliente(nome);

        // TODO: VERIFICAR SE JA EXISTE CLIENTE COM NOME IGUAL
        if (cliente != null)
        {
            Console.WriteLine("Cliente cadastrado com sucesso!");
        }
        else
        {
            Console.WriteLine("Cliente não foi cadastrado, tente novamente!");
        }
    }

    /// <summary>
    /// Método para atender um cliente, verificando se está cadastrado e alocando uma mesa.
    /// </summary>
    public static void AtenderCliente()
    {
        Console.WriteLine("Digite o nome do cliente para atendê-lo:");
        string nome = Console.ReadLine();

        Cliente cliente = restaurante.PesquisarCliente(nome);

        if (cliente == null)
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

    /// <summary>
    /// Método para alocar uma mesa para o cliente.
    /// </summary>
    /// <param name="qntPessoas">Quantidade de pessoas que vão sentar à mesa.</param>
    /// <param name="nome">Nome do cliente.</param>
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
    /// Método principal do programa, exibe o menu principal e gerencia a navegação.
    /// </summary>
    /// <param name="args">Argumentos de linha de comando.</param>
    static void Main(string[] args)
    {
        int opcao;

        // TODO: MENU DO PROGRAMA
        do
        {
            Console.WriteLine("---- MENU PRINCIPAL ----");
            Console.WriteLine("Digite a opção desejada:");
            Console.WriteLine("1) Menu Restaurante");
            Console.WriteLine("2) Menu Café");
            Console.WriteLine("3) Encerrar o programa");
            Console.WriteLine("--------------------------");

            // TODO: TRATAMENTO DE EXCEÇÕES 
            opcao = int.Parse(Console.ReadLine());

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

    /// <summary>
    /// Método para exibir o menu do restaurante e gerenciar as opções.
    /// </summary>
    static void MenuRestaurante()
    {
        int opcaoRestaurante;

        do
        {
            Console.WriteLine("---- MENU RESTAURANTE ----");
            Console.WriteLine("Digite a opção desejada:");
            Console.WriteLine("1) Cadastrar cliente.");
            Console.WriteLine("2) Atender cliente.");
            Console.WriteLine("3) Listar fila de espera.");
            Console.WriteLine("4) Anotar pedido da mesa.");
            Console.WriteLine("5) Adicionar mesa.");
            Console.WriteLine("6) Adicionar produto.");
            Console.WriteLine("7) Ver o cardápio");
            Console.WriteLine("8) Mostrar conta");
            Console.WriteLine("9) Fechar conta");
            Console.WriteLine("10) Voltar ao menu principal");
            Console.WriteLine("--------------------------");

            opcaoRestaurante = int.Parse(Console.ReadLine());

            switch (opcaoRestaurante)
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
                    MostrarConta();
                    break;
                case 9:
                    Console.Clear();
                    FecharConta();
                    break;
                case 10:
                    Console.Clear();
                    return;
                default:
                    Console.WriteLine("Opção inválida, digite novamente!");
                    break;
            }
        } while (opcaoRestaurante != 10);
    }

    /// <summary>
    /// Método para listar a fila de espera do restaurante.
    /// </summary>
    public static void ListarFilaDeEspera()
    {
        Console.WriteLine("Lista de fila de espera:");
        //podemos implementar esse metodo la?
        foreach (var req in restaurante.ListarFilaDeEspera())
        {
            Console.WriteLine($"Cliente: {req.NomeCliente}, Quantidade de pessoas: {req.QtdPessoas}");
        }
    }

    /// <summary>
    /// Método para anotar o pedido de uma mesa.
    /// </summary>
    public static void AnotarPedidoMesa()
    {
        Console.WriteLine("Digite o número da mesa:");
        int numeroMesa = int.Parse(Console.ReadLine());

        Mesa mesa = restaurante.PesquisarMesa(numeroMesa);

        if (mesa == null)
        {
            Console.WriteLine("Mesa não encontrada.");
            return;
        }

        Console.WriteLine("Digite o código do produto:");
        int codigoProduto = int.Parse(Console.ReadLine());
        //seria bacana para buscar os produtos 
        Produto produto = restaurante.PesquisarProduto(codigoProduto);

        if (produto == null)
        {
            Console.WriteLine("Produto não encontrado.");
            return;
        }

        Console.WriteLine("Digite a quantidade:");
        int quantidade = int.Parse(Console.ReadLine());

        ReqMesa req = new ReqMesa(quantidade, mesa.NumeroMesa);
        restaurante.PedirProduto(produto.GetId(), quantidade, req.IdReq);

        Console.WriteLine("Pedido anotado com sucesso.");
    }

    /// <summary>
    /// Método para adicionar uma nova mesa ao restaurante.
    /// </summary>
    public static void AdicionarMesa()
    {
        Console.WriteLine("Digite o número da nova mesa:");
        int numeroMesa = int.Parse(Console.ReadLine());
        Console.WriteLine("Digite a capacidade máxima da mesa:");
        int capacidade = int.Parse(Console.ReadLine());
        //criar uma mesa nova 
        restaurante.CriarMesa(numeroMesa, capacidade);
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
        //pendente a criação de um produto novo 
        restaurante.AddProduto(produto);
        Console.WriteLine("Produto adicionado com sucesso.");
    }

    /// <summary>
    /// Método para mostrar a conta de uma mesa.
    /// </summary>
    public static void MostrarConta()
    {
        Console.WriteLine("Digite o número da mesa:");
        int numeroMesa = int.Parse(Console.ReadLine());
        Mesa mesa = restaurante.PesquisarMesa(numeroMesa);

        if (mesa == null)
        {
            Console.WriteLine("Mesa não encontrada.");
            return;
        }

        Console.WriteLine($"Conta da mesa {numeroMesa}:");
        double total = mesa.CalcularConta();
        Console.WriteLine($"Total: R$ {total:F2}");
    }

    /// <summary>
    /// Método para fechar a conta de uma mesa.
    /// </summary>
    public static void FecharConta()
    {
        Console.WriteLine("Digite o número da mesa:");
        int numeroMesa = int.Parse(Console.ReadLine());
        Mesa mesa = restaurante.PesquisarMesa(numeroMesa);

        if (mesa == null)
        {
            Console.WriteLine("Mesa não encontrada.");
            return;
        }

        double total = restaurante.FecharConta(mesa.numeroMesa);
        Console.WriteLine($"Conta da mesa {numeroMesa} fechada. Total: R$ {total:F2}");
    }

    /// <summary>
    /// Método para exibir o menu do café e gerenciar as opções.
    /// </summary>
    static void MenuCafe()
    {
        int opcaoCafe;

        do
        {
            Console.WriteLine("---- MENU CAFÉ ----");
            Console.WriteLine("Digite a opção desejada:");
            Console.WriteLine("1) Ver o cardápio");
            Console.WriteLine("2) Voltar ao menu principal");
            Console.WriteLine("--------------------------");

            opcaoCafe = int.Parse(Console.ReadLine());

            switch (opcaoCafe)
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
        } while (opcaoCafe != 2);
    }
}
