namespace Algorithms.Sorting;

public sealed class MergeSort
{
    /// <summary>
    /// Итеративный подход не использует стек вызовов (безопасность для больших массивов) и не требует дополнительной памяти для стека вызовов
    /// </summary>
    public static void IterativeSort(int[] array)
    {
        if (array == null)
        {
            throw new ArgumentNullException(nameof(array), "Name cannot be null.");
        }

        if (array.Length <= 1)
            return;

        int n = array.Length;
        int[] tempArray = new int[n];

        // Начинаем с подмассивов размером 1 и увеличиваем в 2 раза на каждом шаге
        for (int size = 1; size < n; size *= 2)
        {
            // Проходим по массиву и сливаем подмассивы
            for (int leftStart = 0; leftStart < n; leftStart += 2 * size)
            {
                int mid = Math.Min(leftStart + size - 1, n - 1);
                int rightEnd = Math.Min(leftStart + 2 * size - 1, n - 1);

                Merge(array, tempArray, leftStart, mid, rightEnd);
            }
        }
    }

    /// <summary>
    /// Рекурсия использует стек вызовов, и при большой глубине рекурсии (для очень больших массивов) может произойти переполнение стека (StackOverflowException).
    /// Рекурсия требует дополнительной памяти для хранения контекста каждого вызова функции
    /// </summary>
    public static void RecursiveSort(int[] array)
    {
        if (array == null)
        {
            throw new ArgumentNullException(nameof(array), "Name cannot be null.");
        }

        if (array.Length <= 1)
            return;

        int[] tempArray = new int[array.Length];
        RecursiveSort(array, tempArray, 0, array.Length - 1);
    }

    private static void RecursiveSort(int[] array, int[] tempArray, int left, int right)
    {
        if (left < right)
        {
            int mid = left + (right - left) / 2;

            // Рекурсивно сортируем левую и правую половины
            RecursiveSort(array, tempArray, left, mid);
            RecursiveSort(array, tempArray, mid + 1, right);

            // Сливаем отсортированные половины
            Merge(array, tempArray, left, mid, right);
        }
    }

    // Метод для слияния двух отсортированных половин
    protected static void Merge(int[] array, int[] tempArray, int left, int mid, int right)
    {
        int i = left;      // Индекс для левой половины
        int j = mid + 1;   // Индекс для правой половины
        int k = left;      // Индекс для временного массива

        // Слияние двух половин в временный массив
        while (i <= mid && j <= right)
        {
            if (array[i] <= array[j])
            {
                tempArray[k] = array[i];
                i++;
            }
            else
            {
                tempArray[k] = array[j];
                j++;
            }
            k++;
        }

        // Копируем оставшиеся элементы из левой половины (если есть)
        while (i <= mid)
        {
            tempArray[k] = array[i];
            i++;
            k++;
        }

        // Копируем оставшиеся элементы из правой половины (если есть)
        while (j <= right)
        {
            tempArray[k] = array[j];
            j++;
            k++;
        }

        // Копируем отсортированные элементы обратно в исходный массив
        for (k = left; k <= right; k++)
        {
            array[k] = tempArray[k];
        }
    }
}
