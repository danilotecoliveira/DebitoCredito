using DebitoCredito.Dominio.Entidades;

namespace DebitoCredito.Dominio.Interfaces.Infra
{
    public interface IContasCorrentes
    {
        bool ValidarContaCadastrada(string contaCorrente);
        bool RealizarDebito(string contaOrigem, decimal valor);
        void InserirLancamento(Lancamento lancamento);
    }
}
