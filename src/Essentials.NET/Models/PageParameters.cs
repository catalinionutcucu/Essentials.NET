namespace Essentials.NET.Models;

public sealed record PageParameters
{
    public required int PageNumber { get; init; }

    public required int PageSize { get; init; }
}
