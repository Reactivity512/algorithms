namespace Algorithms.Search;

public sealed class BinarySearch
{
    /// <summary>Использовать на отсортированном массиве</summary>
    public static int Search<T>(T[] sortedArray, T target) where T : IComparable<T>
    {
        int left = 0;
        int right = sortedArray.Length - 1;

        while (left <= right)
        {
            int mid = left + (right - left) / 2;
            int comparison = sortedArray[mid].CompareTo(target);

            if (comparison == 0)
                return mid; // Элемент найден
            else if (comparison < 0)
                left = mid + 1; // Ищем в правой половине
            else
                right = mid - 1; // Ищем в левой половине
        }

        return -1; // Элемент не найден
    }

    public static int Search<T>(T[] sortedArray, T target, Comparison<T> comparison)
    {
        int left = 0;
        int right = sortedArray.Length - 1;

        while (left <= right)
        {
            int mid = left + (right - left) / 2;
            int result = comparison(sortedArray[mid], target);

            if (result == 0)
                return mid;
            else if (result < 0)
                left = mid + 1;
            else
                right = mid - 1;
        }

        return -1;
    }
}
