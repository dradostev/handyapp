using System;
using System.Threading.Tasks;
using Handy.Bot.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Telegram.Bot.Types;

namespace Handy.App.Controllers
{
    [Route("api/webhook")]
    public class WebhookController : AbstractController
    {
        private readonly ILogger<WebhookController> _logger;
        private readonly IUpdateHandler _updateHandler;

        public WebhookController(ILogger<WebhookController> logger, IUpdateHandler updateHandler)
        {
            _logger = logger;
            _updateHandler = updateHandler;
        }
        
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Update update)
        {
            if (update == null) return Ok();
            await _updateHandler.Handle(update);
            _logger.LogInformation(update.Message.Text);
            
            return Ok();
        }
    }
}