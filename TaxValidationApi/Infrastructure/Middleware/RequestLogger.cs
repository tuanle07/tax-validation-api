using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using TaxValidationApi.Models;

namespace TaxValidationApi.Infrastructure.Middleware
{
    public class RequestLogger
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<RequestLogger> _logger;

        public RequestLogger(RequestDelegate next, ILogger<RequestLogger> logger)
        {
            _next = next ??
                    throw new ArgumentNullException(nameof(next));
            _logger = logger ??
                      throw new ArgumentNullException(nameof(logger));
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next.Invoke(httpContext);
            }
            catch (HttpStatusCodeException ex)
            {
                await HandleException(_logger, ex, httpContext, ex.StatusCode, ex.ContentType);
            }
            catch (Exception ex)
            {
                await HandleException(_logger, ex, httpContext, StatusCodes.Status500InternalServerError,
                    "text/plain");
            }
        }

        private static async Task HandleException(ILogger logger, Exception ex, HttpContext httpContext,
            int statusCode,
            string contentType)
        {
            if (httpContext.Response.HasStarted)
            {
                logger.LogWarning(
                    "The response has already started, the http status code middleware will not be executed.");
            }

            httpContext.Response.StatusCode = statusCode;
            httpContext.Response.ContentType = contentType;

            await httpContext.Response.WriteAsync(ex.Message);
        }
    }
}