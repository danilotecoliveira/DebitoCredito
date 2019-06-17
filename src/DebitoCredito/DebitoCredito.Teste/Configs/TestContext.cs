using System.Net.Http;
using DebitoCredito.Aplicacao;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;

namespace DebitoCredito.Teste.Configs
{
    public class TestContext
    {
        public HttpClient Client { get; set; }
        private TestServer _server;

        public TestContext()
        {
            SetupClient();
        }

        private void SetupClient()
        {
            _server = new TestServer(new WebHostBuilder().UseStartup<Startup>());
            Client = _server.CreateClient();
        }
    }
}
