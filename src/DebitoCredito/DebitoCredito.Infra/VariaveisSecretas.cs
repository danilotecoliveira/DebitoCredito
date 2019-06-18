using Amazon;
using Amazon.SecretsManager;
using Amazon.SecretsManager.Model;
using Microsoft.Extensions.Configuration;

namespace DebitoCredito.Infra
{
    public class VariaveisSecretas : ConfigurationProvider
    {
        public static string ObterValor(string segredo)
        {
            using (IAmazonSecretsManager cliente = new AmazonSecretsManagerClient(RegionEndpoint.USEast1))
            {
                var request = new GetSecretValueRequest()
                {
                    SecretId = segredo
                };

                var response = cliente.GetSecretValueAsync(request).Result;

                return response.SecretString;
            }
        }
    }
}
