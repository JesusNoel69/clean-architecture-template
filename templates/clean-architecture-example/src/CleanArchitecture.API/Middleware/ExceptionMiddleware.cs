using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;
using CleanArchitecture.API.Models;
using CleanArchitecture.Application.Exceptions;
using FluentValidation;

namespace CleanArchitecture.API.Middleware
{
    public class ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
    {
        private readonly RequestDelegate _next = next;
        private readonly ILogger<ExceptionMiddleware> _logger = logger;

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

        private async Task HandleExceptionAsync(HttpContext httpContext, Exception ex)
        {
            HttpStatusCode statusCode = HttpStatusCode.InternalServerError;
            object problem;
            switch (ex)
            {
                case BadRequestException badRequestException:
                    statusCode = HttpStatusCode.BadRequest;
                    problem = new CustomProblemDetails
                    {
                        Title = badRequestException.Message,
                        Status = (int)statusCode,
                        Detail = badRequestException.InnerException?.Message,
                        Type = nameof(badRequestException),
                        Errors = badRequestException.ValidationErrors
                    };
                    break;
                case NotFoundException NotFound:
                    statusCode = HttpStatusCode.NotFound;
                    problem = new CustomProblemDetails
                    {
                        Title = NotFound.Message,
                        Status = (int)statusCode,
                        Type = nameof(NotFoundException),
                        Detail = NotFound.InnerException?.Message,
                    };
                    break;
                case ValidationException validationException:
                    statusCode = HttpStatusCode.BadRequest;

                    problem = new CustomProblemDetails
                    {
                        Title = "Validation Failed",
                        Status = (int)statusCode,
                        Type = nameof(ValidationException),
                        Errors = validationException.Errors
                            .GroupBy(x => x.PropertyName)
                            .ToDictionary(
                                g => g.Key,
                                g => g.Select(x => x.ErrorMessage).ToArray())
                    };

                    break;
                default:
                    statusCode = HttpStatusCode.InternalServerError;
                    problem = new CustomProblemDetails
                    {
                        Title = ex.Message,
                        Status = (int)statusCode,
                        Type = nameof(HttpStatusCode.InternalServerError),
                        //Detail = ex.StackTrace,
                        Detail = "An unexpected error occurred."
                    };
                    break;
            }
            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = (int)statusCode;
            var logMessage = JsonSerializer.Serialize(problem);
            _logger.LogError(logMessage);
            await httpContext.Response.WriteAsJsonAsync((object)problem);
        }
    }
}