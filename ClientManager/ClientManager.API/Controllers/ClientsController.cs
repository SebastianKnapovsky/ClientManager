using ClientManager.Core.DTOs;
using ClientManager.Core.Interfaces;
using ClientManager.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace ClientManager.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientsController : ControllerBase
    {
        private readonly IClientService _clientService;
        private readonly ILogger<ClientsController> _logger;

        public ClientsController(IClientService clientService, ILogger<ClientsController> logger)
        {
            _clientService = clientService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Client>>> GetAll()
        {
            _logger.LogInformation("Fetching all clients");
            var clients = await _clientService.GetAllClientsAsync();
            return Ok(clients);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Client>> Get(Guid id)
        {
            var client = await _clientService.GetClientByIdAsync(id);
            if (client == null)
            {
                _logger.LogWarning("Client with ID {Id} not found", id);
                return NotFound();
            }

            return Ok(client);
        }

        [HttpPost]
        public async Task<ActionResult<Client>> Create([FromBody] ClientDto dto)
        {
            var client = await _clientService.CreateClientAsync(dto);
            _logger.LogInformation("Created new client with ID {Id}", client.Id);
            return CreatedAtAction(nameof(Get), new { id = client.Id }, client);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateClientDto dto)
        {
            if (id != dto.Id)
            {
                _logger.LogWarning("URL ID ({UrlId}) does not match DTO ID ({DtoId})", id, dto.Id);
                return BadRequest("ID mismatch");
            }

            await _clientService.UpdateClientAsync(dto);
            _logger.LogInformation("Updated client with ID {Id}", id);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _clientService.DeleteClientAsync(id);
            _logger.LogInformation("Deleted client with ID {Id}", id);
            return NoContent();
        }
    }
}
