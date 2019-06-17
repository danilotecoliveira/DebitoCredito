using Xunit;
using System;
using System.Linq;
using System.Text;
using System.Net.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;
using DebitoCredito.Teste.Configs;

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
            var content = new StringContent(JsonConvert.SerializeObject("transacao"), Encoding.UTF8, "application/json");

            using (var response = await _testContext.Client.PostAsync($"/api/Transacao", content))
            {
                var guid = response.Headers.GetValues("id-request").FirstOrDefault();

                Assert.True(Guid.TryParse(guid, out _));
            }
        }
    }
}
