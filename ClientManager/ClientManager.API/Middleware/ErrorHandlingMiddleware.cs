using Microsoft.EntityFrameworkCore;
using System.Net;

namespace ClientManager.API.Middleware
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlingMiddleware> _logger;

        public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex, _logger);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext context, Exception exception, ILogger logger)
        {
            var statusCode = HttpStatusCode.InternalServerError;
            var message = "An unexpected error occurred.";
            var traceId = context.TraceIdentifier;

            switch (exception)
            {
                case ArgumentException argEx:
                    statusCode = HttpStatusCode.BadRequest;
                    message = argEx.Message;
                    break;

                case KeyNotFoundException:
                    statusCode = HttpStatusCode.NotFound;
                    message = "The requested resource was not found.";
                    break;

                case DbUpdateException dbEx:
                    statusCode = HttpStatusCode.InternalServerError;
                    message = "A database error occurred.";
                    logger.LogError(dbEx, "Database update error");
                    break;

                default:
                    logger.LogError(exception, "Unhandled exception");
                    break;
            }

            var errorResponse = new
            {
                status = (int)statusCode,
                error = message,
                path = context.Request.Path,
                traceId
            };

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;

            await context.Response.WriteAsJsonAsync(errorResponse);
        }
    }
}
