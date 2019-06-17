using System;
using System.Net;
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
        private readonly ITransacao _transacao;

        public TransacaoController(ITransacao transacao)
        {
            _transacao = transacao;
            _idRequest = Guid.NewGuid().ToString();
        }

        [HttpPost]
        public IActionResult Post([FromBody] Transacao transacao)
        {
            Response.Headers.Add("id-request", _idRequest);

            try
            {
                var validarTransacao = _transacao.ValidarTransacao(transacao);

                return StatusCode(HttpStatusCode.OK.GetHashCode());
            }
            catch (Exception ex)
            {
                return StatusCode(HttpStatusCode.InternalServerError.GetHashCode(), "Erro interno");
            }
        }
    }
}
