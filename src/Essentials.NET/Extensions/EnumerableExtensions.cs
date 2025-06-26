namespace Essentials.NET.Extensions;

public static class EnumerableExtensions
{
    /// <summary>
    /// Executes an action on each item of the enumerable.
    /// </summary>
    /// <returns>The enumerable.</returns>
    public static IEnumerable<TItem> Touch<TItem>(this IEnumerable<TItem> enumerable, Action<TItem> action)
    {
        ArgumentNullException.ThrowIfNull(enumerable);
        ArgumentNullException.ThrowIfNull(action);

        foreach (var item in enumerable)
        {
            action(item);

            yield return item;
        }
    }

    /// <summary>
    /// Determines whether the enumerable is not empty.
    /// </summary>
    /// <returns><c>true</c> if the enumerable is not empty otherwise <c>false</c>.</returns>
    public static bool HasSome<TItem>(this IEnumerable<TItem> enumerable)
    {
        if (enumerable is null)
        {
            return false;
        }

        if (enumerable is ICollection<TItem> collection)
        {
            return collection.Count > 0;
        }

        return enumerable.Any();
    }

    /// <summary>
    /// Determines whether the enumerable is empty.
    /// </summary>
    /// <returns><c>true</c> if the enumerable is empty otherwise <c>false</c>.</returns>
    public static bool HasNone<TItem>(this IEnumerable<TItem> enumerable)
    {
        if (enumerable is null)
        {
            return true;
        }

        if (enumerable is ICollection<TItem> collection)
        {
            return collection.Count == 0;
        }

        return !enumerable.Any();
    }
}
