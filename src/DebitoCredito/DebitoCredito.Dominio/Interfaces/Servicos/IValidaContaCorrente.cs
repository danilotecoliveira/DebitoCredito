namespace DebitoCredito.Dominio.Interfaces.Servicos
{
    public interface IValidaContaCorrente
    {
        bool ValidaContaVazia(string contaCorrente);
        bool ValidaContaCadastrada(string contaCorrente);
    }
}
