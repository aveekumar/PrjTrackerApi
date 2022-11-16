using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace ProjectManagement.Models
{
    public class CustomeErrorHandler
    {
        private readonly RequestDelegate _next;
        public CustomeErrorHandler(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception error)
            {
                // Implement your own exception
                var response = context.Response;
                response.ContentType = "application/json";
                var message = string.Empty;

                var result = error.Message;
                await response.WriteAsync(result);
            }
        }
    }
}
