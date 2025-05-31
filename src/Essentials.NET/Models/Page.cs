namespace Essentials.NET.Models;

public sealed class Page<TItem>
{
    public IEnumerable<TItem> Items { get; init; }

    public int PageNumber { get; init; }

    public int PageSize { get; init; }

    public int TotalItems { get; init; }

    public int TotalPages => (int)Math.Ceiling((double)TotalItems / PageSize);

    public bool HasNextPage => PageNumber < TotalPages;

    public bool HasPreviousPage => PageSize > 1;

    public Page() { }

    public Page(IEnumerable<TItem> items, int pageNumber, int pageSize, int totalItems)
    {
        Items = items;
        PageNumber = pageNumber;
        PageSize = pageSize;
        TotalItems = totalItems;
    }
}

public sealed class PageParameters
{
    public int PageNumber { get; init; }

    public int PageSize { get; init; }

    public PageParameters() { }

    public PageParameters(int pageNumber = 1, int pageSize = 25)
    {
        PageNumber = pageNumber;
        PageSize = pageSize;
    }
}
