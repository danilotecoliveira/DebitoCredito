using System;
using Newtonsoft.Json;

namespace DebitoCredito.Dominio.Entidades
{
    public class ContaCorrente
    {
        [JsonIgnore]
        public string Id { get; set; }

        public string Numero { get; set; }

        [JsonIgnore]
        public decimal SaldoAtual { get; }
    }
}
