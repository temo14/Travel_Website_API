using Contracts;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using System.Net;
using System.Security.Claims;
using System.Security.Principal;

namespace API.Extensions
{
    public class AuthenticationMiddleware
    {
        private readonly RequestDelegate _next;

        // Dependency Injection
        public AuthenticationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, IRepositoryWrapper repository)
        {
            //Get the upload token, which can be customized and extended
            var token = context.Request.Cookies["token"];

            var endpoint = context.GetEndpoint();
            if (endpoint?.Metadata?.GetMetadata<IAllowAnonymous>() is object)
            {
                await _next(context);
                return;
            }
            
            if (token == null)
            {
                context.Response.StatusCode = 401;
                return;
            }

            var userId = repository.JWT.ValidateToken(token);

                context.Items["User"] = repository.User.GetUserById(Guid.Parse(userId));

            var claim = new ClaimsIdentity(new Claim[] {
                new Claim("Id", userId),
            },
                CookieAuthenticationDefaults.AuthenticationScheme);

            context.User = new ClaimsPrincipal(claim);

            await context.AuthenticateAsync();

            await _next(context);
            return;
        }
    }
}
