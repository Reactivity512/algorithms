namespace Algorithms.Sorting;

public static class BubbleSort
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

        int arrLength = array.Length;
        bool swapped;

        // Внешний цикл проходит по всем элементам массива
        for (int i = 0; i < arrLength - 1; i++)
        {
            swapped = false;

            // Внутренний цикл сравнивает соседние элементы и меняет их местами, если нужно
            for (int j = 0; j < arrLength - 1 - i; j++)
            {
                if (array[j] > array[j + 1])
                {
                    // Меняем элементы местами
                    (array[j + 1], array[j]) = (array[j], array[j + 1]);
                    swapped = true;
                }
            }

            // Если на текущем проходе не было обменов, массив уже отсортирован
            if (!swapped)
                 break;
        }
    }
}
