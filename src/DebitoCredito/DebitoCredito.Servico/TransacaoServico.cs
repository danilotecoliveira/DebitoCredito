using System.Threading.Tasks;
using System.Collections.Generic;
using DebitoCredito.Dominio.Entidades;
using DebitoCredito.Dominio.Interfaces.Infra;
using DebitoCredito.Dominio.Interfaces.Servicos;

namespace DebitoCredito.Servico
{
    public class TransacaoServico : ITransacaoServico
    {
        private readonly IContasCorrentes _contasCorrentes;

        public TransacaoServico(IContasCorrentes contasCorrentes)
        {
            _contasCorrentes = contasCorrentes;
        }

        public bool RealizarDebito(string contaOrigem, decimal valor)
        {
            // faz o débito no banco de dados 

            // insere o registro na tabela de lancamentos

            return false;
        }

        public IEnumerable<string> ValidarTransacao(Transacao transacao)
        {
            var erros = new List<string>();

            var validarValor = ValidarValor(transacao.Valor);
            if (!string.IsNullOrWhiteSpace(validarValor))
            {
                erros.Add(validarValor);
            }

            var validarContasCorrentes = ValidarContaCorrente(transacao.ContaOrigem.Numero, transacao.ContaDestino.Numero);
            if (!string.IsNullOrWhiteSpace(validarContasCorrentes))
            {
                erros.Add(validarContasCorrentes);
            }

            var validarContaCadastradaOrigem = _contasCorrentes.ValidarContaCadastrada(transacao.ContaOrigem.Numero);
            var validarContaCadastradaDestino = _contasCorrentes.ValidarContaCadastrada(transacao.ContaDestino.Numero);
            if (!validarContaCadastradaOrigem || !validarContaCadastradaDestino)
            {
                erros.Add("As contas correntes devem estar cadastradas");
            }

            return erros;
        }

        private string ValidarValor(decimal valor)
        {
            return (valor <= 0) ? "O valor da transação não pode ser negativo nem igual a zero": string.Empty;
        }

        private string ValidarContaCorrente(string contaOrigem, string contaDestino)
        {
            return 
                (string.IsNullOrWhiteSpace(contaOrigem) || string.IsNullOrWhiteSpace(contaDestino)) 
                ? "Os números das contas não podem ser vazios" 
                : string.Empty;
        }
    }
}
