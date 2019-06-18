using Xunit;
using System;
using System.Linq;
using DebitoCredito.Infra.Data;
using DebitoCredito.Dominio.Entidades;
using DebitoCredito.Dominio.Interfaces.Infra;
using Microsoft.Extensions.DependencyInjection;

namespace DebitoCredito.Teste.Infra
{
    public class LancamentosTeste
    {
        private readonly ILancamentos _lancamentos;

        public LancamentosTeste()
        {
            var services = new ServiceCollection();
            services.AddTransient<ILancamentos, Lancamentos>();
            services.AddLogging(logger => logger.Services.AddTransient<Lancamentos>());

            var serviceProvider = services.BuildServiceProvider();
            _lancamentos = serviceProvider.GetService<ILancamentos>();
        }

        [Fact]
        public void Testar_Inserir_Lancamento()
        {
            var idTransacao = Guid.NewGuid().ToString();

            var itemNaoExiste = _lancamentos.ObterLancamento(idTransacao);

            Assert.True(!itemNaoExiste.Any());

            var lancamento = new Lancamento
            {
                Acao = "DEBITO",
                IdTransacao = idTransacao,
                NumeroContaCorrente = "0123",
                Valor = 1
            };

            _lancamentos.InserirLancamentoAsync(lancamento);

            var itemExiste = _lancamentos.ObterLancamento(idTransacao);

            Assert.True(itemExiste.Any());
        }
    }
}
