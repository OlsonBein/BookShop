using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Store.BusinessLogicLayer.Common.Extensions;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Store.Presentation.Middlewares
{
    public class LoggerMiddleware
    {
        private readonly RequestDelegate _next;

        private readonly ILogger _logger;

        public LoggerMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
        {
            _next = next;
            loggerFactory.AddFile(Path.Combine(Directory.GetCurrentDirectory(), "logger.txt"));
            _logger = loggerFactory.CreateLogger("FileLogger");
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }
    }
}
