﻿using Dapper;
using System;
using System.Data;
using System.Data.SqlClient;
using DebitoCredito.Dominio.Entidades;
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

        public bool RealizarDebito(string contaOrigem, decimal valor)
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

        public void InserirLancamento(Lancamento lancamento)
        {
            using (var conn = new SqlConnection(VariaveisGlobais.Conn))
            {
                var param = new
                {
                    ACAO = lancamento.Acao,
                    ID = lancamento.Id,
                    IDTRANSACAO = lancamento.IdTransacao,
                    NUMEROCONTACORRENTE = lancamento.NumeroContaCorrente,
                    VALOR = Convert.ToDecimal(lancamento.Valor),
                    DATALANCAMENTO = lancamento.DataLancamento
                };

                var query = @"INSERT INTO LANCAMENTOS 
                                (ID, IDTRANSACAO, ACAO, NUMEROCONTACORRENTE, VALOR, DATALANCAMENTO) 
                            VALUES
                                (@ID, @IDTRANSACAO, @ACAO, @NUMEROCONTACORRENTE, @VALOR, @DATALANCAMENTO)";

                conn.Execute(query, param, commandType: CommandType.Text);
            }
        }

        public bool RealizarCredito(string contaCorrenteDestino)
        {
            using (var conn = new SqlConnection(VariaveisGlobais.Conn))
            {
                var param = new
                {
                    CONTACORRENTE = contaCorrenteDestino
                };

                var resultado = conn.QueryFirstOrDefault<int>("SELECT COUNT(NUMERO) FROM CONTASCORRENTES WHERE NUMERO = @CONTACORRENTE", param, commandType: CommandType.Text);

                return resultado == 1;
            }
        }
    }
}
