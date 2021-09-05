using MessageLogger.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using MessageLogger.Api.Extensions;
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
            // The message should never be null, unless validation property attributes on the model get changed/removed. 
            if (message is null)
            {
                string errorMessage = "Invalid message received (null).";
                
                _logger.LogError(errorMessage);
                
                return BadRequest(errorMessage);
            }

            _logger.LogReceivedMessage(message.MessageId, message.Date, message.Message);

            return Ok();
        }
    }
}
