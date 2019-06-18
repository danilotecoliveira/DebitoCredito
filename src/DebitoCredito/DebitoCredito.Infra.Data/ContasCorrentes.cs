using Dapper;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using DebitoCredito.Dominio.Entidades;
using DebitoCredito.Dominio.Interfaces.Infra;

namespace DebitoCredito.Infra.Data
{
    public class ContasCorrentes : IContasCorrentes
    {
        private readonly ILogger<ContasCorrentes> _logger;

        public ContasCorrentes(ILogger<ContasCorrentes> logger)
        {
            _logger = logger;
        }

        public bool ValidarContaCadastrada(string contaCorrente)
        {
            try
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
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }
        }

        public bool RealizarDebito(string contaOrigem, decimal valor)
        {
            try
            {
                using (var conn = new SqlConnection(VariaveisGlobais.Conn))
                {
                    var param = new
                    {
                        CONTACORRENTE = contaOrigem,
                        VALOR = Convert.ToDecimal(valor)
                    };

                    var resultado = conn.Execute("UPDATE CONTASCORRENTES SET SALDOATUAL = SALDOATUAL - @VALOR WHERE NUMERO = @CONTACORRENTE", param, commandType: CommandType.Text);

                    return resultado == 1;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }
        }

        public bool RealizarCredito(string contaDestino, decimal valor)
        {
            try
            {
                using (var conn = new SqlConnection(VariaveisGlobais.Conn))
                {
                    var param = new
                    {
                        CONTACORRENTE = contaDestino,
                        VALOR = Convert.ToDecimal(valor)
                    };

                    var resultado = conn.Execute("UPDATE CONTASCORRENTES SET SALDOATUAL = SALDOATUAL + @VALOR WHERE NUMERO = @CONTACORRENTE", param, commandType: CommandType.Text);

                    return resultado == 1;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }
        }
    }
}
