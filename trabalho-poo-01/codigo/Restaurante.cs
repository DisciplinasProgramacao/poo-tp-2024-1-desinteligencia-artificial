class Restaurante{
    private List<Mesa> mesas;
    private List<FilaEspera> filasDeEspera;
    private List<ReqMesa> reqMesa;
    private List<Cliente> clientes;

    public Restaurante(){
        this.mesas = new List<Mesa>();
        this.filasDeEspera = new List<FilaEspera>();

        CriarMesas(4, 4); // 4 mesas de capacidade 4
        CriarMesas(4, 6); // 4 mesas de capacidade 6
        CriarMesas(2, 8); // 2 mesas de capacidade 8

        filasDeEspera.Add(new FilaEspera(4));
        filasDeEspera.Add(new FilaEspera(6));
        filasDeEspera.Add(new FilaEspera(8));
    }

    public void CadastrarCliente(Cliente cliente)
    {
        clientes.Add(cliente);
    }

    public void AtenderCliente(Cliente cliente)
    {
        ReqMesa req = new ReqMesa(cliente.QtdPessoas, cliente.Nome);
        reqMesa.Add(req);
        AlocarMesa(req);
    }
    
    public void AlocarMesa(ReqMesa req){
        bool mesaAlocada = false;
        foreach(Mesa mesa in mesas){
            if(req.qtdPessoa <= mesa.capacidadeMaxima && !mesa.estaOcupada){
                mesa.OcuparMesa();
                mesaAlocada = true;
                req.mesa = mesa;
                break;
            }
        }

        if(!mesaAlocada){
            AlocarNaFilaEspera(req); 
        }

    }

    private void AlocarNaFilaEspera(ReqMesa reqMesa)
    {
        FilaEspera filaEscolhida = new FilaEspera(0);
        bool foiEscolhida = false;
        foreach(FilaEspera fila in filasDeEspera)
        {
            if(reqMesa.qtdPessoas <= 4 && fila.capacidadeMesa == 4)
            {
                filaEscolhida = fila;
                foiEscolhida = true;
                break;
            }else if(reqMesa.qtdPessoas <= 6 && fila.capacidadeMesa == 6)
            {
                filaEscolhida = fila;
                foiEscolhida = true;
                break;
            } else if(reqMesa.qtdPessoas <= 8 && fila.capacidadeMesa == 8)
            {
                filaEscolhida = fila;
                foiEscolhida = true;
                break;
            }
        }

        if(foiEscolhida)
        {
            filaEscolhida.AddRequisicao(reqMesa);
        }

    }


    private void CriarMesas(int qtdMesas, int qtdPessoas){
        for ( int i = 0; i < qtdMesas; i++){
            mesas.Add(new Mesa(qtdPessoas));
        }
    }
}
