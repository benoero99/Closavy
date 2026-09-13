using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Closavy.Server.Exceptions;

public sealed class GlobalExceptionHandler(
    IProblemDetailsService problemDetailsService) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        // Log.Error(exception, "Unhandled exception occured");

        httpContext.Response.StatusCode = exception switch
        {
            EntityNotFoundException => StatusCodes.Status404NotFound,
            NameAlreadyTakenException => StatusCodes.Status400BadRequest,
            _ => StatusCodes.Status500InternalServerError
        };

        ProblemDetails problemDetails = new()
        {
            Type = exception.GetType().Name,
            Title = "An error occured",
            Detail = exception.Message,
            Status = httpContext.Response.StatusCode,
        };

        if (exception is CustomExceptionBase customException)
        {
            problemDetails.Title = customException.Title;
        }

        return await problemDetailsService.TryWriteAsync(new ProblemDetailsContext
        {
            HttpContext = httpContext,
            Exception = exception,
            ProblemDetails = problemDetails,
        });
    }
}