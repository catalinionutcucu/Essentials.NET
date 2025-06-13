using FluentValidation.Results;

namespace Essentials.NET.Models;

public sealed record Error
{
    public ErrorType Type { get; }

    public string Code { get; }

    public IEnumerable<string> Issues { get; }

    public Error(ErrorType type, string code, params IEnumerable<string> issues)
    {
        Type = type;
        Code = code;
        Issues = issues;
    }

    /// <summary>
    /// Creates a <see cref = "Error" /> instance representing a request invalid error.
    /// </summary>
    /// <returns>A <see cref = "Error" /> instance representing a request invalid error.</returns>
    public static Error RequestInvalid(string code, params IEnumerable<string> issues)
    {
        return new(ErrorType.RequestInvalid, code, issues);
    }

    /// <summary>
    /// Creates a <see cref = "Error" /> instance representing a request invalid error.
    /// </summary>
    /// <returns>A <see cref = "Error" /> instance representing a request invalid error.</returns>
    public static Error RequestInvalid(string code, ValidationResult validationResult)
    {
        return new(ErrorType.RequestInvalid, code, validationResult.Errors.Select(validationError => validationError.ErrorMessage));
    }

    /// <summary>
    /// Creates a <see cref = "Error" /> instance representing a request not allowed error.
    /// </summary>
    /// <returns>A <see cref = "Error" /> instance representing a request not allowed error.</returns>
    public static Error RequestNotAllowed(string code, params IEnumerable<string> issues)
    {
        return new(ErrorType.RequestNotAllowed, code, issues);
    }

    /// <summary>
    /// Creates a <see cref = "Error" /> instance representing a resource not found error.
    /// </summary>
    /// <returns>A <see cref = "Error" /> instance representing a resource not found error.</returns>
    public static Error ResourceNotFound(string code, params IEnumerable<string> issues)
    {
        return new(ErrorType.ResourceNotFound, code, issues);
    }

    /// <summary>
    /// Creates a <see cref = "Error" /> instance representing a resource conflict error.
    /// </summary>
    /// <returns>A <see cref = "Error" /> instance representing a resource conflict error.</returns>
    public static Error ResourceConflict(string code, params IEnumerable<string> issues)
    {
        return new(ErrorType.ResourceConflict, code, issues);
    }

    /// <summary>
    /// Creates a <see cref = "Error" /> instance representing a failure error.
    /// </summary>
    /// <returns>A <see cref = "Error" /> instance representing a failure error.</returns>
    public static Error Failure(string code, params IEnumerable<string> issues)
    {
        return new(ErrorType.Failure, code, issues);
    }
}

public enum ErrorType
{
    RequestInvalid = 400,
    RequestNotAllowed = 403,
    ResourceNotFound = 404,
    ResourceConflict = 409,
    Failure = 500
}
