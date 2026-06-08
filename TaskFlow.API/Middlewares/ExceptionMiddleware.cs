using System.Net;
using System.Text.Json;
using TaskFlow.API.Exceptions;

namespace TaskFlow.API.Middlewares;

/// <summary>
/// Middleware global pour intercepter toutes les exceptions et les traduire en
/// codes HTTP cohérents.
/// </summary>
public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;

    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erreur non gérée");
            await HandleExceptionAsync(context, ex);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception ex)
    {
        context.Response.ContentType = "application/json";

        var (statusCode, message) = ex switch
        {
            ForbiddenException          => ((int)HttpStatusCode.Forbidden, ex.Message),
            UnauthorizedAccessException => ((int)HttpStatusCode.Unauthorized, ex.Message),
            KeyNotFoundException        => ((int)HttpStatusCode.NotFound, ex.Message),
            InvalidOperationException   => ((int)HttpStatusCode.BadRequest, ex.Message),
            ArgumentException           => ((int)HttpStatusCode.BadRequest, ex.Message),
            _                           => ((int)HttpStatusCode.InternalServerError, "Une erreur est survenue.")
        };

        context.Response.StatusCode = statusCode;
        var result = JsonSerializer.Serialize(new { statusCode, message });
        return context.Response.WriteAsync(result);
    }
}
