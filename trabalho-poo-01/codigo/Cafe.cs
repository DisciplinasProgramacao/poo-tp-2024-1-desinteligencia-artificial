using System;
using System.Collections.Generic;

class Cafe : Loja
{
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
            listaRegistros.Add(req);
            return true;
        }

        return false;
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

        return false;
    }
}