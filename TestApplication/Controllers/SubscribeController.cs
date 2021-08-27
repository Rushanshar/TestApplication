
using System.Threading.Tasks;
using BL.Interfaces;
using BL.Models.TransferModels;
using Microsoft.AspNetCore.Mvc;

namespace TestApp.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class SubscribeController : ControllerBase
    {
        private readonly ISubscriberService _subscribeService;

        public SubscribeController(ISubscriberService subscribeService)
        {
            _subscribeService = subscribeService;
        }

        /// <summary>
        /// Подписать клиента
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(bool), 200)]
        public async Task<IActionResult> SubscribeClientAsync(ClientSubscribeRequest request)
        {
            return Ok(await _subscribeService.SubscribeClientAsync(request.SubscriberId, request.ClientForSubscribeId));
        }

        /// <summary>
        /// Отписать клиента
        /// </summary>
        [HttpDelete]
        [ProducesResponseType(typeof(bool), 200)]
        public async Task<IActionResult> UnsubscribeClientAsync(ClientSubscribeRequest request)
        {
            return Ok(await _subscribeService.UnsubscribeClientAsync(request.SubscriberId, request.ClientForSubscribeId));
        }
    }
}
