using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using DebitoCredito.Dominio.Entidades;

namespace DebitoCredito.Dominio.Interfaces.Infra
{
    public interface ILancamentos
    {
        Task InserirLancamentoAsync(Lancamento lancamento);
        IEnumerable<Lancamento> ObterLancamento(string idTransacao);
    }
}
