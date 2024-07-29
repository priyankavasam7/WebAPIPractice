using Azure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace CustomersAPI.Middleware
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class ApiKeyAuthMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration _configuratoin;

        public ApiKeyAuthMiddleware(RequestDelegate next,IConfiguration configuration)
        {
            _next = next;
            _configuratoin = configuration;
        }


        public async Task Invoke(HttpContext httpContext)
        {
            if(!httpContext.Request.Headers.TryGetValue("X-API-KEY",out var actualHeaderKeyValue))
            {
                httpContext.Response.StatusCode = 401;
                await httpContext.Response.WriteAsJsonAsync(new { error = "Key missing" });
                return;
            }
            var secretApi = _configuratoin["ApiKey"];
            if(!actualHeaderKeyValue.Equals(secretApi))
            {
                httpContext.Response.StatusCode = 401;
                await httpContext.Response.WriteAsJsonAsync(new { error = "Invalid Key" });
                return;
            }
            await _next(httpContext);
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class ApiKeyAuthMiddlewareExtensions
    {
        public static IApplicationBuilder UseApiKeyAuthMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ApiKeyAuthMiddleware>();
        }
    }
}
