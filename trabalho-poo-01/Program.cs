using System;
using System.Collections.Generic;

class Program
{
    static Restaurante restaurante = new Restaurante();

    /// <summary>
    /// Cadastra um novo cliente no sistema.
    /// </summary>
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

    /// <summary>
    /// Atende um cliente, verificando se ele está cadastrado e alocando uma mesa para ele.
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
    /// Aloca um cliente e seus acompanhantes a uma mesa disponível ou os move para a fila de espera.
    /// </summary>
    /// <param name="qntPessoas">Quantidade de pessoas que ocuparão a mesa.</param>
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
    /// Exibe o cardápio do restaurante.
    /// </summary>
    public static void VerCardapio()
    {
        Console.WriteLine("---- CARDÁPIO ----");
        restaurante.ExibirCardapio();
    }

    /// <summary>
    /// Inclui um produto ao pedido de um cliente.
    /// </summary>
    public static void IncluirProdutoAoPedido()
    {
        Console.WriteLine("Digite o ID do Pedido:");
        int idPedido = int.Parse(Console.ReadLine());

        Console.WriteLine("Digite o ID do Produto:");
        int idProduto = int.Parse(Console.ReadLine());

        Console.WriteLine("Digite a quantidade:");
        int quantidade = int.Parse(Console.ReadLine());

        restaurante.PedirProduto(idProduto, idPedido, quantidade);

        Console.WriteLine("Produto adicionado ao pedido com sucesso!");
    }

    /// <summary>
    /// Fecha a conta de uma mesa e exibe o valor total.
    /// </summary>
    public static void FecharConta()
    {
        Console.WriteLine("Digite o ID da Mesa:");
        int idMesa = int.Parse(Console.ReadLine());

        double valorConta = restaurante.FecharConta(idMesa);
        if (valorConta >= 0)
        {
            Console.WriteLine($"Conta fechada com sucesso. Valor total: R$ {valorConta}");
        }
        else
        {
            Console.WriteLine("Erro ao fechar a conta. Mesa não encontrada.");
        }
    }

    /// <summary>
    /// Método principal que exibe o menu do restaurante e processa as opções selecionadas pelo usuário.
    /// </summary>
    /// <param name="args">Argumentos de linha de comando.</param>
    static void Main(string[] args)
    {
        int opcao;

        do
        {
            Console.WriteLine("---- MENU RESTAURANTE ----");
            Console.WriteLine("Digite a opção desejada:");
            Console.WriteLine("1) Cadastrar cliente.");
            Console.WriteLine("2) Atender cliente.");
            Console.WriteLine("3) Ver cardápio.");
            Console.WriteLine("4) Incluir produto ao pedido.");
            Console.WriteLine("5) Fechar conta.");
            Console.WriteLine("6) Encerrar o programa.");
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
                    VerCardapio();
                    break;
                case 4:
                    Console.Clear();
                    IncluirProdutoAoPedido();
                    break;
                case 5:
                    Console.Clear();
                    FecharConta();
                    break;
                case 6:
                    Console.Clear();
                    Console.WriteLine("Encerrando programa...");
                    break;
                default:
                    Console.WriteLine("Opção inválida, digite novamente!");
                    break;
            }
        } while (opcao != 6);

        Console.WriteLine("Obrigado por usar o OO Comidinhas Veganas!");
    }
}
