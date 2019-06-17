namespace DebitoCredito.Dominio.Entidades
{
    public class Transacao
    {
        public ContaCorrente ContaOrigem { get; set; }
        public ContaCorrente ContaDestino { get; set; }
        public decimal Valor { get; set; }
    }
}
