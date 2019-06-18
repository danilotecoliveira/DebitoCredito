using System;

namespace DebitoCredito.Dominio.Entidades
{
    public class Lancamento
    {
        public Lancamento()
        {
            Id = Guid.NewGuid().ToString();
            DataLancamento = DateTime.Now;
        }

        public string Id { get; set; }
        public string IdTransacao { get; set; }
        public string Acao { get; set; }
        public string NumeroContaCorrente { get; set; }
        public decimal Valor { get; set; }
        public DateTime DataLancamento { get; set; }
    }
}
