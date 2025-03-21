namespace Algorithms.Sorting;

public static class SelectionSort
{
    /// <summary>
    /// Неэффективен для реальных задач, не используйте.
    /// </summary>
    public static void Sort(int[] array)
    {
        if (array == null)
        {
            throw new ArgumentNullException(nameof(array), "Name cannot be null.");
        }

        if (array.Length <= 1)
            return;

        int n = array.Length;

        for (int i = 0; i < n - 1; i++)
        {
            // Находим индекс минимального элемента в неотсортированной части массива
            int minIndex = i;
            for (int j = i + 1; j < n; j++)
            {
                if (array[j] < array[minIndex])
                {
                    minIndex = j;
                }
            }

            // Меняем местами минимальный элемент и первый элемент неотсортированной части
            if (minIndex != i)
            {
                (array[minIndex], array[i]) = (array[i], array[minIndex]);
            }
        }
    }
}
