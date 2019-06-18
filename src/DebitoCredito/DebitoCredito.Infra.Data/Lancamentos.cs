using System;
using Dapper;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using DebitoCredito.Dominio.Entidades;
using DebitoCredito.Dominio.Interfaces.Infra;

namespace DebitoCredito.Infra.Data
{
    public class Lancamentos : ILancamentos
    {
        private readonly ILogger<Lancamentos> _logger;

        public Lancamentos(ILogger<Lancamentos> logger)
        {
            _logger = logger;
        }

        public async Task InserirLancamentoAsync(Lancamento lancamento)
        {
            try
            {
                using (var conn = new SqlConnection(VariaveisGlobais.Conn))
                {
                    var param = new
                    {
                        ACAO = lancamento.Acao,
                        ID = lancamento.Id,
                        IDTRANSACAO = lancamento.IdTransacao.ToString(),
                        NUMEROCONTACORRENTE = lancamento.NumeroContaCorrente,
                        VALOR = Convert.ToDecimal(lancamento.Valor),
                        DATALANCAMENTO = lancamento.DataLancamento
                    };

                    var query = @"INSERT INTO LANCAMENTOS 
                                (ID, IDTRANSACAO, ACAO, NUMEROCONTACORRENTE, VALOR, DATALANCAMENTO) 
                            VALUES
                                (@ID, @IDTRANSACAO, @ACAO, @NUMEROCONTACORRENTE, @VALOR, @DATALANCAMENTO)";

                    await conn.ExecuteAsync(query, param, commandType: CommandType.Text);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }

        public IEnumerable<Lancamento> ObterLancamento(string idTransacao)
        {
            try
            {
                using (var conn = new SqlConnection(VariaveisGlobais.Conn))
                {
                    var param = new
                    {
                        IDTRANSACAO = idTransacao.ToString()
                    };

                    var query = @"SELECT 
                                    [ID] = CONVERT(VARCHAR(50), ID), 
	                                [IDTRANSACAO] = CONVERT(VARCHAR(50), IDTRANSACAO), 
                                    ACAO, 
                                    NUMEROCONTACORRENTE, 
                                    VALOR, 
                                    DATALANCAMENTO 
                                FROM 
                                    LANCAMENTOS 
                                WHERE 
                                    IDTRANSACAO = @IDTRANSACAO";

                    return conn.Query<Lancamento>(query, param, commandType: CommandType.Text);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                return new List<Lancamento>();
            }
        }
    }
}
