using System.Threading.Tasks;
using DebitoCredito.Dominio.Entidades;

namespace DebitoCredito.Dominio.Interfaces.Servicos
{
    public interface ITransacao
    {
        Task RealizarTransacao(ContaCorrente contaOrigem, ContaCorrente contaDestino, Lancamento lancamento);
    }
}
