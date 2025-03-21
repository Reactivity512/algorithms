namespace Algorithms.Sorting;

public class QuickSortAsync : QuickSort
{
    public static async Task RecursiveSortAsync(int[] array)
    {
        if (array == null)
        {
            throw new ArgumentNullException(nameof(array), "Name cannot be null.");
        }

        if (array.Length <= 1)
            return;

        await SortAsync(array, 0, array.Length - 1);
    }

    public static async Task IterativeSortAsync(int[] array)
    {
        if (array == null)
        {
            throw new ArgumentNullException(nameof(array), "Name cannot be null.");
        }

        if (array.Length <= 1)
            return;

        // Стек для хранения границ подмассивов
        Stack<(int low, int high)> stack = new Stack<(int, int)>();
        stack.Push((0, array.Length - 1)); // Начальные границы всего массива

        // Список задач для параллельного выполнения
        var tasks = new List<Task>();

        while (stack.Count > 0 || tasks.Count > 0)
        {
            // Если есть задачи, ожидаем их завершения
            if (stack.Count == 0 && tasks.Count > 0)
            {
                await Task.WhenAll(tasks);
                tasks.Clear();
            }

            // Извлекаем границы текущего подмассива
            if (stack.Count > 0)
            {
                var (low, high) = stack.Pop();

                if (low < high)
                {
                    // Разделяем подмассив и получаем индекс опорного элемента
                    int pivotIndex = Partition(array, low, high);

                    // Добавляем границы левого и правого подмассивов в стек
                    stack.Push((low, pivotIndex - 1)); // Левый подмассив
                    stack.Push((pivotIndex + 1, high)); // Правый подмассив

                    // Запускаем задачу для сортировки одного из подмассивов
                    tasks.Add(SortChunkAsync(array, stack));
                }
            }
        }
    }

    private static async Task SortChunkAsync(int[] array, Stack<(int low, int high)> stack)
    {
        while (stack.Count > 0)
        {
            var (low, high) = stack.Pop();

            if (low < high)
            {
                int pivotIndex = Partition(array, low, high);

                stack.Push((low, pivotIndex - 1));
                stack.Push((pivotIndex + 1, high));

                // Добавляем асинхронную паузу, чтобы позволить другим задачам выполняться
                await Task.Yield();
            }
        }
    }

    private static async Task SortAsync(int[] array, int low, int high)
    {
        if (low < high)
        {
            // Разделяем массив и получаем индекс опорного элемента
            int pivotIndex = Partition(array, low, high);

            // Рекурсивно сортируем левую и правую части асинхронно
            var leftTask = SortAsync(array, low, pivotIndex - 1);
            var rightTask = SortAsync(array, pivotIndex + 1, high);

            // Ожидаем завершения обеих задач
            await Task.WhenAll(leftTask, rightTask);
        }
    }
}