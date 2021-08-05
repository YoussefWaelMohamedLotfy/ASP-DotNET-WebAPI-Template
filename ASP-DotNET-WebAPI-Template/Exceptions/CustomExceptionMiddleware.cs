using System;
using System.Net;
using System.Threading.Tasks;
using ASP_DotNET_WebAPI_Template.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace ASP_DotNET_WebAPI_Template.Exceptions
{
    public class CustomExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<CustomExceptionMiddleware> _logger;

        public CustomExceptionMiddleware(RequestDelegate next, ILogger<CustomExceptionMiddleware> logger)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }   
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext httpContext, Exception ex)
        {
            httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            httpContext.Response.ContentType = "application/json";

            var error = new GlobalError()
            {
                StatusCode = httpContext.Response.StatusCode,
                Message = $"Exception from custom middleware: {ex.Message}",
                Path = httpContext.Request.Path
            }.ToString();

            _logger.LogError(error);
            return httpContext.Response.WriteAsync(error);
        }
    }
}