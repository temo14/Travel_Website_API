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
            var token = context.Request.Cookies["Token"];

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

            var isValid = repository.JWT.ValidateToken(token);

            if (isValid != null)
            {
                context.Items["User"] = repository.User.GetProfile(Guid.Parse(isValid));
            }
            else
            {
                context.Response.StatusCode = 400;
                await context.Response.WriteAsync("Invalid username or password.");
            }
            var claim = new ClaimsIdentity(new Claim[] { new Claim("Id", isValid) },
                     CookieAuthenticationDefaults.AuthenticationScheme);

            context.User = new ClaimsPrincipal(claim);
            await context.AuthenticateAsync();

            await _next(context);
        }
    }
}
