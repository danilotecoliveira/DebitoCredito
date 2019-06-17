using System;

namespace DebitoCredito.Dominio.Entidades
{
    public class Lancamento
    {
        public Lancamento()
        {
            Id = Guid.NewGuid();
            DataLancamento = DateTime.Now;
        }

        public Guid Id { get; private set; }
        public Guid IdTransacao { get; set; }
        public string Acao { get; set; }
        public string NumeroContaCorrente { get; set; }
        public decimal Valor { get; set; }
        public DateTime DataLancamento { get; private set; }
    }
}
