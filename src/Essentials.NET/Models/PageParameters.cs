using System.Diagnostics.CodeAnalysis;

namespace Essentials.NET.Models;

public sealed record PageParameters
{
    public required int PageNumber { get; init; }

    public required int PageSize { get; init; }

    public PageParameters() { }

    [SetsRequiredMembers]
    public PageParameters(int pageNumber, int pageSize)
    {
        PageNumber = pageNumber;
        PageSize = pageSize;
    }
}
