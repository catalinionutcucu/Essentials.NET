namespace Essentials.NET.Mediator.Models;

internal sealed record RequestType
{
    internal required Type Type { get; init; }

    internal required Type? ResultType { get; init; }
}
