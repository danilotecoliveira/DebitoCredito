using Xunit;
using System;
using System.Linq;
using System.Text;
using System.Net.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;
using DebitoCredito.Teste.Configs;
using DebitoCredito.Dominio.Entidades;

namespace DebitoCredito.Teste.Controller
{
    public class TransacaoControllerTeste
    {
        private readonly TestContext _testContext;

        public TransacaoControllerTeste()
        {
            _testContext = new TestContext();
        }

        [Fact]
        public async Task Teste_Transacao()
        {
            var transacao = new
            {
                ContaOrigem = new ContaCorrente { Id = Guid.NewGuid().ToString(), Numero = "" },
                ContaDestino = new ContaCorrente { Id = Guid.NewGuid().ToString(), Numero = "" },
                Valor = 0
            };

            var content = new StringContent(JsonConvert.SerializeObject(transacao), Encoding.UTF8, "application/json");

            using (var response = await _testContext.Client.PostAsync($"/api/Transacao", content))
            {
                var guid = response.Headers.GetValues("id-request").FirstOrDefault();

                Assert.True(Guid.TryParse(guid, out _));
            }
        }

        [Theory]
        [InlineData("", "", -1, 400)]
        [InlineData("0123", "3210", 1, 200)]
        public async Task Teste_Valor_Negativo(string contaOrigem, string contaDestino, decimal valor, int statusCode)
        {
            var transacao = new
            {
                ContaOrigem = new ContaCorrente { Id = Guid.NewGuid().ToString(), Numero = contaOrigem },
                ContaDestino = new ContaCorrente { Id = Guid.NewGuid().ToString(), Numero = contaDestino },
                Valor = valor
            };

            var content = new StringContent(JsonConvert.SerializeObject(transacao), Encoding.UTF8, "application/json");

            using (var response = await _testContext.Client.PostAsync($"/api/Transacao", content))
            {
                var guid = response.Headers.GetValues("id-request").FirstOrDefault();

                if (response.IsSuccessStatusCode)
                {
                    Assert.Equal(statusCode, response.StatusCode.GetHashCode());
                }

                Assert.Equal(statusCode, response.StatusCode.GetHashCode());
                Assert.True(Guid.TryParse(guid, out _));
            }
        }
    }
}
