using System;
using System.Net;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using DebitoCredito.Dominio.Entidades;
using DebitoCredito.Dominio.Interfaces.Servicos;

namespace DebitoCredito.Aplicacao.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransacaoController : ControllerBase
    {
        private readonly string _idRequest;
        private readonly ITransacaoServico _transacaoServico;

        public TransacaoController(ITransacaoServico transacaoServico)
        {
            _transacaoServico = transacaoServico;
            _idRequest = Guid.NewGuid().ToString();
        }

        [HttpPost]
        public IActionResult Post([FromBody] Transacao transacao)
        {
            Response.Headers.Add("id-request", _idRequest);

            try
            {
                var validarTransacao = _transacaoServico.ValidarTransacao(transacao);

                if (validarTransacao.Any())
                {
                    return StatusCode(HttpStatusCode.BadRequest.GetHashCode(), validarTransacao);
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
