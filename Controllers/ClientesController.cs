using Microsoft.AspNetCore.Mvc;
using LHPetsAPI.Models;

namespace LHPetsAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientesController : ControllerBase
    {
        private static readonly List<Cliente> _clientes = new()
        {
            new Cliente { Id = 1, Nome = "Carlos Silva", Cpf = "123.456.789-00", Email = "carlos@email.com" },
            new Cliente { Id = 2, Nome = "Ana Souza", Cpf = "987.654.321-11", Email = "ana@email.com" }
        };

        [HttpGet]
        public ActionResult<IEnumerable<Cliente>> Get()
        {
            return Ok(_clientes);
        }

        [HttpPost]
        public ActionResult<Cliente> Post([FromBody] Cliente novoCliente)
        {
            if (novoCliente == null)
            {
                return BadRequest("Dados inválidos.");
            }

            novoCliente.Id = _clientes.Count > 0 ? _clientes.Max(c => c.Id) + 1 : 1;
            _clientes.Add(novoCliente);

            return CreatedAtAction(nameof(Get), new { id = novoCliente.Id }, novoCliente);
        }
    }
}