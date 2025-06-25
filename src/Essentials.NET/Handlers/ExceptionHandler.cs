using Essentials.NET.Models;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Essentials.NET.Handlers;

public sealed class ExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken = default)
    {
        if (exception is NotImplementedException)
        {
            httpContext.Response.StatusCode = 501;

            await httpContext.Response.WriteAsJsonAsync(
                new ProblemDetails
                {
                    Status = 501,
                    Title = "Not Implemented",
                    Type = "https://tools.ietf.org/html/rfc7231#section-6.6.2"
                },
                cancellationToken);
        }
        else
        {
            httpContext.Response.StatusCode = 500;

            await httpContext.Response.WriteAsJsonAsync(
                new ProblemDetails
                {
                    Status = 500,
                    Title = "Internal Server Error",
                    Type = "https://tools.ietf.org/html/rfc7231#section-6.6.1"
                },
                cancellationToken);
        }

        return true;
    }
}
