using MessageLogger.Api.Models;
using MessageLogger.Api.Services;
using Microsoft.AspNetCore.Mvc;
using System;

namespace MessageLogger.Api.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}")]
    [ApiVersion("1.0")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IUserService _userService;

        public AuthenticationController(IUserService userService)
        {
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
        }

        [HttpPost("authenticate")]
        public IActionResult Authenticate(AuthenticationRequest request)
        {
            var response = _userService.Authenticate(request);

            if (response == null)
            {
                return BadRequest("Username, or password is incorrect.");
            }

            return Ok(response);
        }
    }
}
