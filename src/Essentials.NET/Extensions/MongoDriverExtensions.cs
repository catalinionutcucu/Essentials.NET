using Essentials.NET.Models;
using MongoDB.Driver;

namespace Essentials.NET.Extensions;

public static class MongoDriverExtensions
{
    /// <summary>
    /// Returns asynchronously a page of items of type <typeparamref name = "TItem" /> from a Mongo Driver query.
    /// </summary>
    /// <returns>A page of items of type <typeparamref name = "TItem" />.</returns>
    /// <exception cref = "ArgumentException">Thrown if the page number or the page size is negative.</exception>
    public static async Task<Page<TItem>> ToPageAsync<TItem>(this IFindFluent<TItem, TItem> query, int pageNumber, int pageSize, CancellationToken cancellationToken = default)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(pageNumber);
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(pageSize);

        var totalItems = await query.CountDocumentsAsync(cancellationToken);

        var items = await query
                          .Skip((pageNumber - 1) * pageSize)
                          .Limit(pageSize)
                          .ToListAsync(cancellationToken);

        return new Page<TItem>
        {
            Items = items,
            PageNumber = pageNumber,
            PageSize = pageSize,
            TotalItems = totalItems
        };
    }

    /// <summary>
    /// Returns asynchronously a page of items of type <typeparamref name = "TItem" /> from a Mongo Driver query.
    /// </summary>
    /// <returns>A page of items of type <typeparamref name = "TItem" />.</returns>
    /// <exception cref = "ArgumentException">Thrown if the page number or the page size is negative.</exception>
    public static async Task<Page<TItem>> ToPageAsync<TItem>(this IFindFluent<TItem, TItem> query, PageParameters pageParameters, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(pageParameters);

        return await query.ToPageAsync(pageParameters.PageNumber, pageParameters.PageSize, cancellationToken);
    }
}
