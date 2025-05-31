using FluentValidation.Results;

namespace Essentials.NET.Extensions;

public static class FluentValidationExtensions
{
    /// <summary>
    /// Extracts the validation error messages from a <see cref = "ValidationResult" /> instance.
    /// </summary>
    /// <returns>The validation error messages.</returns>
    public static IEnumerable<string> GetValidationErrorMessages(this ValidationResult validationResult)
    {
        return validationResult.Errors.Select(validationError => validationError.ErrorMessage);
    }

    /// <summary>
    /// Determines whether a <see cref = "ValidationResult" /> instance indicates a successful validation.
    /// </summary>
    /// <returns>Whether the validation is successful.</returns>
    public static bool IsSuccess(this ValidationResult validationResult)
    {
        return validationResult.IsValid;
    }

    /// <summary>
    /// Determines whether a <see cref = "ValidationResult" /> instance indicates a failed validation.
    /// </summary>
    /// <returns>Whether the validation is failed.</returns>
    public static bool IsFailure(this ValidationResult validationResult)
    {
        return !validationResult.IsValid;
    }
}
