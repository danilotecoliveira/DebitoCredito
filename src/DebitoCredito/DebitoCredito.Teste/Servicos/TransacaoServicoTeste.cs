using DebitoCredito.Servico;
using Microsoft.Extensions.DependencyInjection;
using DebitoCredito.Dominio.Interfaces.Servicos;
using DebitoCredito.Dominio.Entidades;
using System;
using System.Threading.Tasks;
using Xunit;
using System.Linq;

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

        [Fact]
        public void Testa_Valor_Negativo()
        {
            var transacao = new Transacao
            {
                ContaOrigem = new ContaCorrente { Id = Guid.NewGuid(), Numero = "" },
                ContaDestino = new ContaCorrente { Id = Guid.NewGuid(), Numero = "" },
                Valor = -1
            };

            var erro = _transacaoServico.ValidarTransacao(transacao);

            Assert.Contains("O valor da transação não pode ser negativo", erro);
        }
    }
}
