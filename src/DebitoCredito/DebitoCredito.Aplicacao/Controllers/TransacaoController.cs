using System;
using System.Net;
using System.Linq;
using DebitoCredito.Infra;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using DebitoCredito.Dominio.Entidades;
using DebitoCredito.Dominio.Interfaces.Servicos;

namespace DebitoCredito.Aplicacao.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransacaoController : ControllerBase
    {
        private readonly Guid _idRequest;
        private readonly ITransacaoServico _transacaoServico;

        public TransacaoController(ITransacaoServico transacaoServico)
        {
            _transacaoServico = transacaoServico;
            _idRequest = Guid.NewGuid();
        }

        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(IEnumerable<string>), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.InternalServerError)]
        [HttpPost]
        public IActionResult Post([FromBody] Transacao transacao)
        {
            Response.Headers.Add("id-request", _idRequest.ToString());
            VariaveisGlobais.Transacao = _idRequest;

            try
            {
                var validarTransacao = _transacaoServico.ValidarTransacao(transacao);

                if (validarTransacao.Any())
                {
                    return StatusCode(HttpStatusCode.BadRequest.GetHashCode(), validarTransacao);
                }

                var realizarTransacao = _transacaoServico.RealizarTransacao(transacao);

                if (!realizarTransacao)
                {
                    return StatusCode(HttpStatusCode.InternalServerError.GetHashCode(), "Erro ao realizar a transação");
                }

                return StatusCode(HttpStatusCode.OK.GetHashCode());
            }
            catch (Exception ex)
            {
                return StatusCode(HttpStatusCode.InternalServerError.GetHashCode(), "Erro interno");
            }
        }
    }
}
