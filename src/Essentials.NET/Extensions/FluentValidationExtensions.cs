using FluentValidation.Results;

namespace Essentials.NET.Extensions;

public static class FluentValidationExtensions
{
    /// <summary>
    /// Determines whether the validation succeeded.
    /// </summary>
    /// <returns><c>true</c> if the validation succeeded otherwise <c>false</c>.</returns>
    public static bool IsSuccess(this ValidationResult validationResult)
    {
        ArgumentNullException.ThrowIfNull(validationResult);

        return validationResult.IsValid;
    }

    /// <summary>
    /// Determines whether the validation failed.
    /// </summary>
    /// <returns><c>true</c> if the validation failed otherwise <c>false</c>.</returns>
    public static bool IsFailure(this ValidationResult validationResult)
    {
        ArgumentNullException.ThrowIfNull(validationResult);

        return !validationResult.IsValid;
    }
}
