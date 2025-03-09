using CompanyContacts.Application.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Text.Json;

namespace CompanyContacts.Application.Middleware;

public sealed class ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
{
    private readonly RequestDelegate _next = next;
    private readonly ILogger<ExceptionMiddleware> _logger = logger;

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unhandled exception occurred.");
            await HandleExceptionAsync(context, ex);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";

        var response = new
        {
            message = exception.Message,
            statusCode = (int)HttpStatusCode.InternalServerError
        };

        if(exception is NotFoundException)
        {
            context.Response.StatusCode = (int)HttpStatusCode.NotFound;
            response = new
            {
                message = exception.Message,
                statusCode = context.Response.StatusCode
            };
        }else if(exception is ValidationException){
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            response = new
            {
                message = exception.Message,
                statusCode = context.Response.StatusCode
            };
        }

        var jsonResponse = JsonSerializer.Serialize(response);
        return context.Response.WriteAsync(jsonResponse);
    }
}