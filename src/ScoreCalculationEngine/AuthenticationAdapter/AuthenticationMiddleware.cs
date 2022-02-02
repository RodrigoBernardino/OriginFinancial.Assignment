using Microsoft.AspNetCore.Http;
using System.Net;

namespace AuthenticationAdapter
{
    internal class AuthenticationMiddleware
    {
        private readonly RequestDelegate _next;

        public AuthenticationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            if (!httpContext.Request.Path.HasValue || httpContext.Request.Path.Value.Contains("swagger"))
            {
                await _next(httpContext);
                return;
            }

            var requestApiAccessKey = httpContext.Request.Headers["ApiKey"].ToString();

            if (!string.IsNullOrWhiteSpace(requestApiAccessKey)
                || requestApiAccessKey == "b466ad75-19c6-45d0-8bf2-bf0071f8301e")
            {
                await _next(httpContext);
            }
            else
            {
                httpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                await httpContext.Response.WriteAsync("Unauthorized: Invalid Api Key.");
            }
        }
    }
}
