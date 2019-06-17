using System;
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
        public void Post([FromBody] string value)
        {
            Response.Headers.Add("id-request", _idRequest);
        }
    }
}
