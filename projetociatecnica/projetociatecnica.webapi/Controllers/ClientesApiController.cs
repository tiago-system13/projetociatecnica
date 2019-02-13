using aplicacao.Servicos.ServicoDePessoa;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace projetociatecnica.webapi.Controllers
{    
    [RoutePrefix("api/cliente")]
    public class ClientesApiController : ApiController
    {
        private readonly IServicoPessoa _servCliente;

        public ClientesApiController(IServicoPessoa servCliente)
        {
            _servCliente = servCliente;
        }

        [HttpGet]
        [Route("")]
        public IEnumerable<ProjecaoDadosPessoa> ListarClientes()
        {
            return _servCliente.ListarTodasPessoas();
        }
    }
}
