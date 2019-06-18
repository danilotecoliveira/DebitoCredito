using Xunit;
using System;
using DebitoCredito.Servico;
using DebitoCredito.Dominio.Entidades;
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
            services.AddLogging(configuration => configuration.Services.AddTransient<TransacaoServico>());

            var serviceProvider = services.BuildServiceProvider();
            _transacaoServico = serviceProvider.GetService<ITransacaoServico>();
        }

        [Theory]
        [InlineData("O valor da transação não pode ser negativo", -1)]
        public void Testa_Valor_Negativo(string msgErro, int valor)
        {
            var transacao = new Transacao
            {
                ContaOrigem = new ContaCorrente { Id = Guid.NewGuid(), Numero = "" },
                ContaDestino = new ContaCorrente { Id = Guid.NewGuid(), Numero = "" },
                Valor = valor
            };

            var erro = _transacaoServico.ValidarTransacao(transacao);

            Assert.Contains(msgErro, erro);
        }
    }
}
