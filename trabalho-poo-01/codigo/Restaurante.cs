using System;
using System.Collections.Generic;

/// <summary>
/// Classe que representa um restaurante, gerenciando mesas, requisições, clientes e cardápio.
/// </summary>
class Restaurante : Loja
{
    private List<ReqMesa> listaEspera;

    /// <summary>
    /// Inicializa uma nova instância da classe <see cref="Restaurante"/>, criando mesas e listas necessárias.
    /// </summary>
    public Restaurante()
    {
        this.listaEspera = new List<ReqMesa>();
    }

    /// <summary>
    /// Processa uma requisição de mesa, alocando uma mesa disponível ou adicionando à lista de espera.
    /// </summary>
    /// <param name="req">A requisição de mesa.</param>
    /// <returns>True se a mesa foi alocada com sucesso; caso contrário, False.</returns>
    public override bool ProcessarRequisicao(ReqMesa req)
    {
        foreach (Mesa mesa in mesas)
        {
            if (AlocarMesa(req, mesa))
            {
                return true;
            }
        }

        listaEspera.Add(req);
        return false;
    }

    /// <summary>
    /// Aloca uma mesa específica para uma requisição se disponível.
    /// </summary>
    /// <param name="req">A requisição de mesa.</param>
    /// <param name="mesa">A mesa a ser alocada.</param>
    /// <returns>True se a mesa foi alocada com sucesso; caso contrário, False.</returns>
    protected override bool AlocarMesa(ReqMesa req, Mesa mesa)
    {
        if (mesa.VerificarDisponibilidade(req.QtdPessoas))
        {
            mesa.OcuparMesa();
            req.AtribuirMesaARequisicao(mesa.NumeroMesa);
            listaRegistros.Add(req);
            return true;
        }

        return false;
    }

    /// <summary>
    /// Processa a lista de espera, alocando mesas conforme disponibilidade.
    /// </summary>
    /// <returns>String indicando as requisições processadas com sucesso.</returns>
    public string ProcessarListaDeEspera()
    {
        string resposta = "";
        foreach (ReqMesa req in listaEspera)
        {
            foreach (Mesa mesa in mesas)
            {
                if (AlocarMesa(req, mesa))
                {
                    listaEspera.Remove(req);
                    resposta += $"Requisição {req.IdReq} alocada com sucesso.\n";
                }
            }
        }

        return resposta;
    }
}
