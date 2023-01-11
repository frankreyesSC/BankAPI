using Microsoft.AspNetCore.Mvc;
using BankAPI.Services;
using BankAPI.Data.BankModels;
using Microsoft.AspNetCore.Authorization;

namespace BankAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class ClientController : ControllerBase
    {
        private readonly ClientService _service;

        public ClientController(ClientService client)
        {
            _service = client;
        }

        [HttpGet]
        public async Task<IEnumerable<Client>> Get()
        {
            return await _service.GetAll();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Client>> GetById(int id)
        {
            var client = await _service.GetById(id);

            if (client is null)
            {
                return NotFound(new { message = "El Cliente no existe" });
            }

            return client;
        }

        [HttpPost]
        public async Task<IActionResult> Create(Client client)
        {
            var newClient = await _service.Create(client);

            return CreatedAtAction(nameof(GetById), new { id = newClient.Id }, newClient);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Client client)
        {
            if (id != client.Id)
            {
                return BadRequest();
            }
            var clientToUpdate = await _service.GetById(id);

            if (clientToUpdate is not null)
            {
                await _service.UpdateClient(clientToUpdate);
                return NoContent();

            }
            else
            {
                return NotFound();
            }


        }

        [HttpDelete("id")]
        public async Task<IActionResult> Delete(int id)
        {
            var existingClient = await _service.GetById(id);

            if (existingClient is not null)
            {
                await _service.Delete(id);
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }

    }
}
