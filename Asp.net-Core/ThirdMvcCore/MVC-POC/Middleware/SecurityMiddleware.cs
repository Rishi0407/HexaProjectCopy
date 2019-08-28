using System;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;

namespace MiddlewareDemo.Middleware
{
    public class SecurityMiddleware
    {
        private RequestDelegate _next;
        public SecurityMiddleware(RequestDelegate next)
        {
            this._next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            await context.Response.WriteAsync("<br/>Security Middleware - Request processed!");
            await _next.Invoke(context);
            await context.Response.WriteAsync("<br/> Security middleware - Response Processed!");
        }
    }

    public static class MiddelwareExtensions
    {
        public static void UseSecurity(this IApplicationBuilder app)
        {
            app.UseMiddleware<SecurityMiddleware>();
        }
    }
}