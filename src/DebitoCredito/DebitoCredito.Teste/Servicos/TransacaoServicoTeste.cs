using Xunit;
using System;
using DebitoCredito.Servico;
using DebitoCredito.Infra.Data;
using DebitoCredito.Dominio.Entidades;
using DebitoCredito.Dominio.Interfaces.Infra;
using Microsoft.Extensions.DependencyInjection;
using DebitoCredito.Dominio.Interfaces.Servicos;

namespace DebitoCredito.Teste.Servicos
{
    public class TransacaoServicoTeste
    {
        private readonly ITransacaoServico _transacaoServico;

        public TransacaoServicoTeste()
        {
            var services = new ServiceCollection();
            services.AddTransient<ITransacaoServico, TransacaoServico>();
            services.AddTransient<IContasCorrentes, ContasCorrentes>();
            services.AddLogging(configuration => configuration.Services.AddTransient<TransacaoServico>());

            var serviceProvider = services.BuildServiceProvider();
            _transacaoServico = serviceProvider.GetService<ITransacaoServico>();
        }

        [Theory]
        [InlineData("O valor da transação não pode ser negativo nem igual a zero", "0123", "3210", -1)]
        [InlineData("O valor da transação não pode ser negativo nem igual a zero", "0123", "3210", 0)]
        [InlineData("Os números das contas não podem ser vazios", "0123", "", 1)]
        [InlineData("Os números das contas não podem ser vazios", "", "3210", 1)]
        [InlineData("As contas correntes devem estar cadastradas", "01234", "3210", 1)]
        [InlineData("As contas correntes devem estar cadastradas", "0123", "43210", 1)]
        public void Testa_Transacao(string msgErro, string contaOrigem, string contaDestino, int valor)
        {
            var transacao = new Transacao
            {
                ContaOrigem = new ContaCorrente { Id = Guid.NewGuid(), Numero = contaOrigem },
                ContaDestino = new ContaCorrente { Id = Guid.NewGuid(), Numero = contaDestino },
                Valor = valor
            };

            var erro = _transacaoServico.ValidarTransacao(transacao);

            Assert.Contains(msgErro, erro);
        }
    }
}
