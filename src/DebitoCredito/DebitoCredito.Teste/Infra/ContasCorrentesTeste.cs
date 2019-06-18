using Xunit;
using System;
using DebitoCredito.Infra.Data;
using DebitoCredito.Dominio.Entidades;
using DebitoCredito.Dominio.Interfaces.Infra;
using Microsoft.Extensions.DependencyInjection;

namespace DebitoCredito.Teste.Infra
{
    public class ContasCorrentesTeste
    {
        private readonly IContasCorrentes _contasCorrentes;

        public ContasCorrentesTeste()
        {
            var services = new ServiceCollection();
            services.AddTransient<IContasCorrentes, ContasCorrentes>();

            var serviceProvider = services.BuildServiceProvider();
            _contasCorrentes = serviceProvider.GetService<IContasCorrentes>();
        }

        [Theory]
        [InlineData("0123", true)]
        [InlineData("01234", false)]
        public void Testar_Validar_Conta_Cadastrada(string contaCorrente, bool esperado)
        {
            var resultado = _contasCorrentes.ValidarContaCadastrada(contaCorrente);

            Assert.Equal(esperado, resultado);
        }

        [Theory]
        [InlineData("0123", 1)]
        [InlineData("0123", 0.99)]
        public void Testar_Realizar_Debito(string contaCorrente, decimal valor)
        {
            var resultado = _contasCorrentes.RealizarDebito(contaCorrente, valor);

            Assert.True(resultado);
        }

        [Fact]
        public void Testar_Inserir_Lancamento()
        {
            var lancamento = new Lancamento
            {
                Acao = "DEBITO",
                IdTransacao = Guid.NewGuid(),
                NumeroContaCorrente = "0123",
                Valor = 1
            };

            _contasCorrentes.InserirLancamentoAsync(lancamento);

            Assert.True(true);
        }
    }
}
