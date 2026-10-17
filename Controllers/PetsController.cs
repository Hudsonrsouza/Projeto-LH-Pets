using Microsoft.AspNetCore.Mvc;
using LHPetsAPI.Models;

namespace LHPetsAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PetsController : ControllerBase
    {
        private static readonly List<Pet> _pets = new()
        {
            new Pet { Id = 1, Nome = "Rex", Especie = "Cão", Raca = "Labrador", ClienteId = 1 },
            new Pet { Id = 2, Nome = "Miau", Especie = "Gato", Raca = "Siamês", ClienteId = 1 },
            new Pet { Id = 3, Nome = "Thor", Especie = "Cão", Raca = "Poodle", ClienteId = 2 }
        };

        [HttpGet]
        public ActionResult<IEnumerable<Pet>> Get()
        {
            return Ok(_pets);
        }

        [HttpPost]
        public ActionResult<Pet> Post([FromBody] Pet novoPet)
        {
            if (novoPet == null)
            {
                return BadRequest("Dados inválidos.");
            }

            novoPet.Id = _pets.Count > 0 ? _pets.Max(p => p.Id) + 1 : 1;
            _pets.Add(novoPet);

            return CreatedAtAction(nameof(Get), new { id = novoPet.Id }, novoPet);
        }

        [HttpGet("cliente/{clienteId}")]
        public ActionResult<IEnumerable<Pet>> GetPorCliente(int clienteId)
        {
            var petsDoCliente = _pets.Where(p => p.ClienteId == clienteId).ToList();
            return Ok(petsDoCliente);
        }
    }
}