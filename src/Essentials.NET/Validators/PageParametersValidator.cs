using Essentials.NET.Models;
using FluentValidation;

namespace Essentials.NET.Validators;

public class PageParametersValidator : AbstractValidator<PageParameters>
{
    public PageParametersValidator()
    {
        RuleFor(x => x.PageNumber)
            .GreaterThan(0)
            .WithMessage(context => $"{nameof(context.PageNumber)} invalid, must be greater than 0.");

        RuleFor(x => x.PageSize)
            .GreaterThan(0)
            .WithMessage(context => $"{nameof(context.PageSize)} invalid, must be greater than 0.");
    }
}
