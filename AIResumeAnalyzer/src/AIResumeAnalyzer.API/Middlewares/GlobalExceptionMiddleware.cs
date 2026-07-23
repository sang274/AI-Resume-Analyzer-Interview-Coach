using AIResumeAnalyzer.Application.Common.Exceptions;
using AIResumeAnalyzer.Application.Exceptions;
using FluentValidation;
using System.Net;
using System.Text.Json;

namespace AIResumeAnalyzer.API.Middlewares
{
    public class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionMiddleware> _logger;

        public GlobalExceptionMiddleware(
            RequestDelegate next,
            ILogger<GlobalExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(
            HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                await HandleExceptionAsync(
                    context,
                    ex);
            }
        }

        private static async Task HandleExceptionAsync(
            HttpContext context,
            Exception exception)
        {
            var response = new ErrorResponse();

            switch (exception)
            {
                case ValidationException validationException:
                    response.StatusCode =
                        (int)HttpStatusCode.BadRequest;

                    response.Message =
                        string.Join(
                            ", ",
                            validationException.Errors
                                .Select(x => x.ErrorMessage));
                    break;

                case BadRequestException:
                    response.StatusCode =
                        (int)HttpStatusCode.BadRequest;

                    response.Message =
                        exception.Message;
                    break;

                case NotFoundException:
                    response.StatusCode =
                        (int)HttpStatusCode.NotFound;

                    response.Message =
                        exception.Message;
                    break;

                case UnauthorizedAccessException:
                    response.StatusCode =
                        (int)HttpStatusCode.Unauthorized;

                    response.Message =
                        "Unauthorized.";
                    break;

                default:
                    response.StatusCode =
                        (int)HttpStatusCode.InternalServerError;

                    response.Message =
                        "Internal server error.";
                    break;
            }

            context.Response.ContentType =
                "application/json";

            context.Response.StatusCode =
                response.StatusCode;

            await context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
    }
}
