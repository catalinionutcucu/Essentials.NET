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
    /// Determines whether the enumerable contains some elements that match the specified predicate.
    /// </summary>
    /// <returns><c>true</c> if the enumerable contains some elements that match the specified predicate otherwise <c>false</c>.</returns>
    public static bool HasSome<TItem>(this IEnumerable<TItem> enumerable, Func<TItem, bool> predicate)
    {
        ArgumentNullException.ThrowIfNull(predicate);

        if (enumerable is null)
        {
            return false;
        }

        return enumerable.Any(predicate);
    }

    /// <summary>
    /// Determines whether the enumerable contains some non-null elements.
    /// </summary>
    /// <returns><c>true</c> if the enumerable contains some non-null elements otherwise <c>false</c>.</returns>
    public static bool HasSomeNotNull<TItem>(this IEnumerable<TItem> enumerable)
    {
        return enumerable.HasSome(item => item is not null);
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

    /// <summary>
    /// Determines whether the enumerable contains no elements that match the specified predicate.
    /// </summary>
    /// <returns><c>true</c> if the enumerable contains no elements that match the specified predicate otherwise <c>false</c>.</returns>
    public static bool HasNone<TItem>(this IEnumerable<TItem> enumerable, Func<TItem, bool> predicate)
    {
        ArgumentNullException.ThrowIfNull(predicate);

        if (enumerable is null)
        {
            return true;
        }

        return !enumerable.Any(predicate);
    }

    /// <summary>
    /// Determines whether the enumerable contains no non-null elements.
    /// </summary>
    /// <returns><c>true</c> if the enumerable contains no non-null elements otherwise <c>false</c>.</returns>
    public static bool HasNoneNotNull<TItem>(this IEnumerable<TItem> enumerable)
    {
        return enumerable.HasNone(item => item is not null);
    }
}
