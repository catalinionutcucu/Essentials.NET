using FluentValidation.Results;

namespace Essentials.NET.Extensions.FluentValidation;

public static class ValidationResultExtensions
{
    public static bool IsSuccess(this ValidationResult validationResult)
    {
        ArgumentNullException.ThrowIfNull(validationResult);

        return validationResult.IsValid;
    }

    public static bool IsFailure(this ValidationResult validationResult)
    {
        ArgumentNullException.ThrowIfNull(validationResult);

        return !validationResult.IsValid;
    }
}
