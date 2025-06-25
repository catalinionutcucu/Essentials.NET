using System.Diagnostics.CodeAnalysis;

namespace Essentials.NET.Models;

public sealed record Page<TItem>
{
    public required IEnumerable<TItem> Items { get; init; }

    public required int PageNumber { get; init; }

    public required int PageSize { get; init; }

    public required long TotalItems { get; init; }

    public int TotalPages => (int)Math.Ceiling((double)TotalItems / PageSize);

    public bool HasNextPage => PageNumber < TotalPages;

    public bool HasPreviousPage => PageSize > 1;

    public Page() { }

    [SetsRequiredMembers]
    public Page(IEnumerable<TItem> items, int pageNumber, int pageSize, long totalItems)
    {
        Items = items;
        PageNumber = pageNumber;
        PageSize = pageSize;
        TotalItems = totalItems;
    }
}
