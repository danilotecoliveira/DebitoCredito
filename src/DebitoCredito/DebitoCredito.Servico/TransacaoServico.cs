using System.Threading.Tasks;
using System.Collections.Generic;
using DebitoCredito.Dominio.Entidades;
using DebitoCredito.Dominio.Interfaces.Servicos;

namespace DebitoCredito.Servico
{
    public class TransacaoServico : ITransacaoServico
    {
        public Task RealizarTransacao(ContaCorrente contaOrigem, ContaCorrente contaDestino, Lancamento lancamento)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<string> ValidarTransacao(Transacao transacao)
        {
            var erros = new List<string>();

            var validarValor = ValidarValor(transacao.Valor);
            if (!string.IsNullOrWhiteSpace(validarValor))
            {
                erros.Add(validarValor);
            }

            return erros;
        }

        private string ValidarValor(decimal valor)
        {
            return (valor < 0) ? "O valor da transação não pode ser negativo": string.Empty;
        }
    }
}
