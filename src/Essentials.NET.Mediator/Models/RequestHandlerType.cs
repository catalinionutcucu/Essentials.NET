namespace Essentials.NET.Mediator.Models;

internal sealed record RequestHandlerType
{
    internal required Type Type { get; init; }

    internal required RequestType RequestType { get; init; }
}
