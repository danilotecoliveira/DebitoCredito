using Dapper;
using System.Data;
using System.Data.SqlClient;
using DebitoCredito.Dominio.Interfaces.Infra;

namespace DebitoCredito.Infra.Data
{
    public class ContasCorrentes : IContasCorrentes
    {
        public bool ValidarContaCadastrada(string contaCorrente)
        {
            using (var conn = new SqlConnection(VariaveisGlobais.Conn))
            {
                var param = new
                {
                    CONTACORRENTE = contaCorrente
                };

                var resultado = conn.QueryFirstOrDefault<int>("SELECT COUNT(NUMERO) FROM CONTASCORRENTES WHERE NUMERO = @CONTACORRENTE", param, commandType: CommandType.Text);

                return resultado == 1;
            }
        }
    }
}
