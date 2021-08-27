
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BL.Interfaces;
using BL.Models.TransferModels;
using Microsoft.AspNetCore.Mvc;

namespace TestApp.Controllers
{
    /// <summary>
    /// Контроллер для работы с клиентами
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IClientService _clientService;

        public ClientController(IClientService clientService)
        {
            _clientService = clientService;
        }

        /// <summary>
        /// Получение клиента
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ClientResponse), 200)]
        public async Task<IActionResult> GetClientAsync(long id)
        {
            var client = await _clientService.TryGetClientAsync(id);
            if (client == null)
                return NoContent();
            return Ok(new ClientResponse(client.Id, client.Name, client.GetSubscribers()));
        }

        /// <summary>
        /// Получение топа наиболее популярных клиентов
        /// </summary>
        [HttpGet("top")]
        [ProducesResponseType(typeof(IEnumerable<TopClientResponse>), 200)]
        public async Task<IActionResult> GetTopClientsAsync(int take = 10)
        {
            var topClients = await _clientService.GetTopClientsAsync(take);

            return Ok(topClients.Select(client => new TopClientResponse(client.Id, client.Name, client.GetSubscribers()))
                .OrderByDescending(q => q.SubscribersCount));
        }

        /// <summary>
        /// Добавление клиента
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(ClientCreateResponse), 200)]
        public async Task<IActionResult> AddClientAsync(ClientCreateRequest request)
        {
            var clientId = await _clientService.AddNewClientAsync(request.Name);
            var response = new ClientCreateResponse {Id = clientId};
            return Ok(response);
        }
    }
}
