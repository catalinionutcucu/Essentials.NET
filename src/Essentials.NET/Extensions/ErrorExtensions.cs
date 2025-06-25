using Essentials.NET.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Essentials.NET.Extensions;

public static class ErrorExtensions
{
    /// <summary>
    /// Maps a <see cref = "Error" /> instance to an HTTP response of type <see cref = "IResult" /> based on the error type.
    /// </summary>
    /// <returns>An HTTP response of type <see cref = "IResult" />.</returns>
    public static IResult ToHttpResponse(this Error error)
    {
        ArgumentNullException.ThrowIfNull(error);

        return Results.Problem(
            new ProblemDetails
            {
                Status = error.Type switch
                {
                    ErrorType.RequestInvalid => 400,
                    ErrorType.RequestNotAllowed => 403,
                    ErrorType.ResourceNotFound => 404,
                    ErrorType.ResourceConflict => 409,
                    _ => 500
                },
                Title = error.Type switch
                {
                    ErrorType.RequestInvalid => "Bad Request",
                    ErrorType.RequestNotAllowed => "Forbidden",
                    ErrorType.ResourceNotFound => "Not Found",
                    ErrorType.ResourceConflict => "Conflict",
                    _ => "Internal Server Error"
                },
                Type = error.Type switch
                {
                    ErrorType.RequestInvalid => "https://tools.ietf.org/html/rfc7231#section-6.5.1",
                    ErrorType.RequestNotAllowed => "https://tools.ietf.org/html/rfc7231#section-6.5.3",
                    ErrorType.ResourceNotFound => "https://tools.ietf.org/html/rfc7231#section-6.5.4",
                    ErrorType.ResourceConflict => "https://tools.ietf.org/html/rfc7231#section-6.5.8",
                    _ => "https://tools.ietf.org/html/rfc7231#section-6.6.1"
                },
                Extensions = new Dictionary<string, object?>
                {
                    {
                        "error", new Dictionary<string, object?>
                        {
                            { "code", error.Code },
                            { "issues", error.Issues.ToArray() }
                        }
                    }
                }
            });
    }
}
