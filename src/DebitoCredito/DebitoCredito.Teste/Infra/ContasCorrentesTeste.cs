using Xunit;
using DebitoCredito.Infra.Data;
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
    }
}
