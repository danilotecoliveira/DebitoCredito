using System;
using System.ComponentModel.DataAnnotations;
using System.Net;
using DebitoCredito.Dominio.Entidades;
using Microsoft.AspNetCore.Mvc;

namespace DebitoCredito.Aplicacao.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransacaoController : ControllerBase
    {
        private readonly string _idRequest;

        public TransacaoController()
        {
            _idRequest = Guid.NewGuid().ToString();
        }

        [HttpPost]
        public IActionResult Post([FromBody] Transacao transacao)
        {
            Response.Headers.Add("id-request", _idRequest);

            return StatusCode(HttpStatusCode.OK.GetHashCode());
        }
    }
}
