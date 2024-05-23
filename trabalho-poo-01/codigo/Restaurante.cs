class Restaurante{
    private List<Mesa> mesas;
    private List<FilaEspera> filaDeEspera;

    public Restaurante(){
        this.mesas = new List<Mesa>();
        this.filaDeEspera = new List<FilaEspera>();

        CriarMesas(4, 4); // 4 mesas de capacidade 4
        CriarMesas(4, 6); // 4 mesas de capacidade 6
        CriarMesas(2, 8); // 2 mesas de capacidade 8

        filaDeEspera.Add(new FilaEspera(4));
        filaDeEspera.Add(new FilaEspera(6));
        filaDeEspera.Add(new FilaEspera(8));
    }
    
    public void AlocarMesa(int qtdPessoa, string nome){
        bool mesaAlocada = false;
        foreach(Mesa mesa in mesas){
            if(mesa.capacidade <= qtdPessoa && !mesa.isOcupada){
                mesa.ocuparMesa();
                mesaAlocada = true;
                break;
            }
        }

        if(!mesaAlocada){
            Mesa mesa = mesas.Find(mesa => mesa.capacidade <= qtdPessoa);
            ReqMesa req = new ReqMesa(qtdPessoa, nome, mesa);
        }
    }

    public void DesalocarDaFilaEspera (int idReq) {
        foreach(FilaEspera fila in filaDeEspera){
            foreach(ReqMesa req in fila.requisicoes){
                if(req.idReq == idReq){
                    req.FecharRequisicao();
                    fila.RemoveRequisicao(req);
                    return;
                }
            }
        }
    }
    private void AlocarNaFilaEspera (ReqMesa reqMesa){
        FilaEspera fila = filaDeEspera.Find(fila => fila.capacidadeMesa <= qtdPessoa);
        fila.AddReq(reqMesa);
    }

    private void CriarMesas(int qtdMesas, int qtdPessoas){
        for ( int i = 0; i < qtdMesas; i++){
            mesas.Add(new Mesa(qtdPessoas));
        }
    }
}
