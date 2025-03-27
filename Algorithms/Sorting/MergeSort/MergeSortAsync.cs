using System.Collections.Generic;

namespace Algorithms.Sorting;

public sealed class MergeSortAsync : MergeSort
{
    /// <summary>
    /// Асинхронная реализация лучше подходит для больших массивов, где можно эффективно использовать многопоточность.
    /// Итеративный подход не использует стек вызовов (безопасность для больших массивов) и не требует дополнительной памяти для стека вызовов
    /// </summary>
    public static async Task IterativeSortAsync(int[] array)
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
            // Создаем задачи для параллельного слияния подмассивов
            var tasks = new System.Collections.Generic.List<Task>();

            for (int leftStart = 0; leftStart < n; leftStart += 2 * size)
            {
                int mid = Math.Min(leftStart + size - 1, n - 1);
                int rightEnd = Math.Min(leftStart + 2 * size - 1, n - 1);

                // Запускаем слияние асинхронно
                tasks.Add(MergeAsync(array, tempArray, leftStart, mid, rightEnd));
            }

            // Ожидаем завершения всех задач
            await Task.WhenAll(tasks);
        }
    }

    // Асинхронный метод для слияния двух отсортированных подмассивов
    private static Task MergeAsync(int[] array, int[] tempArray, int left, int mid, int right)
    {
        return Task.Run(() =>
        {
            int i = left;      // Индекс для левого подмассива
            int j = mid + 1;   // Индекс для правого подмассива
            int k = left;      // Индекс для временного массива

            // Слияние двух подмассивов
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

            // Копируем оставшиеся элементы из левого подмассива (если есть)
            while (i <= mid)
            {
                tempArray[k] = array[i];
                i++;
                k++;
            }

            // Копируем оставшиеся элементы из правого подмассива (если есть)
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
        });
    }

    /// <summary>
    /// Асинхронная реализация лучше подходит для больших массивов, где можно эффективно использовать многопоточность.
    /// Рекурсия использует стек вызовов, и при большой глубине рекурсии (для очень больших массивов) может произойти переполнение стека (StackOverflowException).
    /// Рекурсия требует дополнительной памяти для хранения контекста каждого вызова функции
    /// </summary>
    public static async Task RecursiveSortAsync(int[] array)
    {
        if (array == null)
        {
            throw new ArgumentNullException(nameof(array), "Name cannot be null.");
        }

        if (array.Length <= 1)
            return;

        int[] tempArray = new int[array.Length];
        await RecursiveSortAsync(array, tempArray, 0, array.Length - 1);
    }

    private static async Task RecursiveSortAsync(int[] array, int[] tempArray, int left, int right)
    {
        if (left < right)
        {
            int mid = left + (right - left) / 2;

            // Рекурсивно сортируем левую и правую половины асинхронно
            var leftTask = RecursiveSortAsync(array, tempArray, left, mid);
            var rightTask = RecursiveSortAsync(array, tempArray, mid + 1, right);

            // Ожидаем завершения обеих задач
            await Task.WhenAll(leftTask, rightTask);

            // Сливаем отсортированные половины
            Merge(array, tempArray, left, mid, right);
        }
    }
}
