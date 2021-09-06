using MessageLogger.Api.Configuration;
using MessageLogger.Api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageLogger.Api.Middleware
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly AppSettings _appSettings;

        public JwtMiddleware(RequestDelegate next, IOptions<AppSettings> appSettings)
        {
            _next = next;
            _appSettings = appSettings.Value;
        }

        public async Task Invoke(HttpContext context, IUserService userService)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            var validatedToken = ValidateToken(token);

            if (validatedToken.IsValid)
            {
                AttachToHttpContext(context, userService, validatedToken.Token);
            }

            await _next(context);
        }

        private (bool IsValid, JwtSecurityToken Token) ValidateToken(string token)
        {
            if (token is null)
            {
                return (false, null);
            }

            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);

            var tokenHandler = new JwtSecurityTokenHandler();

            SecurityToken securityToken = null;
            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                },
                out securityToken);
            }
            catch (Exception ex)
            {
                // TODO: Log errors
                return (false, null);
            }

            if (securityToken is null)
            {
                // TODO: Log error
                return (false, null);
            }

            return (true, (JwtSecurityToken)securityToken);
        }

        private static void AttachToHttpContext(HttpContext context, IUserService userService, JwtSecurityToken securityToken)
        {
            var userId = int.Parse(securityToken.Claims.First(x => x.Type == "userid").Value);

            context.Items["User"] = userService.GetUserById(userId);
        }
    }
}
