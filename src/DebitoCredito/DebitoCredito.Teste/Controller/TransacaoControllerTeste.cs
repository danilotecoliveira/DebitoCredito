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
                ContaOrigem = new ContaCorrente { Id = Guid.NewGuid(), Numero = "" },
                ContaDestino = new ContaCorrente { Id = Guid.NewGuid(), Numero = "" },
                Valor = 0
            };

            var content = new StringContent(JsonConvert.SerializeObject(transacao), Encoding.UTF8, "application/json");

            using (var response = await _testContext.Client.PostAsync($"/api/Transacao", content))
            {
                var guid = response.Headers.GetValues("id-request").FirstOrDefault();

                Assert.True(Guid.TryParse(guid, out _));
            }
        }

        [Fact]
        public async Task Teste_Valor_Negativo()
        {
            var transacao = new
            {
                ContaOrigem = new ContaCorrente { Id = Guid.NewGuid(), Numero = "" },
                ContaDestino = new ContaCorrente { Id = Guid.NewGuid(), Numero = "" },
                Valor = -1
            };

            var content = new StringContent(JsonConvert.SerializeObject(transacao), Encoding.UTF8, "application/json");

            using (var response = await _testContext.Client.PostAsync($"/api/Transacao", content))
            {
                var guid = response.Headers.GetValues("id-request").FirstOrDefault();

                Assert.Equal(400, response.StatusCode.GetHashCode());
                Assert.True(Guid.TryParse(guid, out _));
            }
        }
    }
}
