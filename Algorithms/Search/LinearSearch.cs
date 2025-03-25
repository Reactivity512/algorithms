namespace Algorithms.Search;

public sealed class LinearSearch
{
    /// <summary>Использовать на Не отсортированном массиве</summary>
    public static T? Search<T>(T[] array, T target) where T : IEquatable<T>
    {
        foreach (var item in array)
        {
            if (item.Equals(target))
            {
                return item;
            }
        }
        return default;
    }

    public static T? Search<T>(T[] array, Predicate<T> predicate)
    {
        foreach (var item in array)
        {
            if (predicate(item))
            {
                return item;
            }
        }
        return default;
    }
}
