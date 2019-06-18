using System.Collections.Generic;
using DebitoCredito.Dominio.Entidades;

namespace DebitoCredito.Dominio.Interfaces.Servicos
{
    public interface ITransacaoServico
    {
        IEnumerable<string> ValidarTransacao(Transacao transacao);
        bool RealizarDebito(string contaOrigem, decimal valor);
    }
}
