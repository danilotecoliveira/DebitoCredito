using System.Threading.Tasks;
using System.Collections.Generic;
using DebitoCredito.Dominio.Entidades;

namespace DebitoCredito.Dominio.Interfaces.Servicos
{
    public interface ITransacaoServico
    {
        IEnumerable<string> ValidarTransacao(Transacao transacao);
        Task RealizarTransacao(ContaCorrente contaOrigem, ContaCorrente contaDestino, Lancamento lancamento);
    }
}
