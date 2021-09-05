using MessageLogger.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;

namespace MessageLogger.Api.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public class MessagesController : ControllerBase
    {
        private readonly ILogger<MessagesController> _logger;

        public MessagesController(ILogger<MessagesController> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpPost]
        public IActionResult Post(LogMessage message)
        {
            if (message is null)
            {
                return BadRequest("The message could not be parsed.");
            }

            _logger.LogInformation($"{message.MessageId}, {message.Date}, {message.Message} ");

            return Ok();
        }
    }
}
