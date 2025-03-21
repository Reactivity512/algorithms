namespace Algorithms.Sorting;

public class QuickSort
{
    public static void IterativeSort(int[] array)
    {
        if (array == null)
        {
            throw new ArgumentNullException(nameof(array), "Name cannot be null.");
        }

        if (array.Length <= 1)
            return;

        // Стек для хранения границ подмассивов
        Stack<(int low, int high)> stack = new();
        stack.Push((0, array.Length - 1)); // Начальные границы всего массива

        while (stack.Count > 0)
        {
            // Извлекаем границы текущего подмассива
            var (low, high) = stack.Pop();

            if (low < high)
            {
                // Разделяем подмассив и получаем индекс опорного элемента
                int pivotIndex = Partition(array, low, high);

                // Добавляем границы левого и правого подмассивов в стек
                stack.Push((low, pivotIndex - 1)); // Левый подмассив
                stack.Push((pivotIndex + 1, high)); // Правый подмассив
            }
        }
    }

    public static void RecursiveSort(int[] array)
    {
        if (array == null)
        {
            throw new ArgumentNullException(nameof(array), "Name cannot be null.");
        }

        if (array.Length <= 1)
            return;

        Sort(array, 0, array.Length - 1);
    }

    private static void Sort(int[] array, int low, int high)
    {
        if (low < high)
        {
            // Разделяем массив и получаем индекс опорного элемента
            int pivotIndex = Partition(array, low, high);

            // Рекурсивно сортируем левую и правую части
            Sort(array, low, pivotIndex - 1);
            Sort(array, pivotIndex + 1, high);
        }
    }

    protected static int Partition(int[] array, int low, int high)
    {
        // Выбираем опорный элемент (в данном случае последний элемент)
        int pivot = array[high];
        int i = low - 1; // Индекс для элемента, меньшего опорного

        for (int j = low; j < high; j++)
        {
            // Если текущий элемент меньше или равен опорному
            if (array[j] <= pivot)
            {
                i++;
                if (i != j) {
                    (array[j], array[i]) = (array[i], array[j]);
                }
            }
        }

        // Помещаем опорный элемент на правильное место
        if (i + 1 != high) {
            (array[high], array[i + 1]) = (array[i + 1], array[high]);
        }

        // Возвращаем индекс опорного элемента
        return i + 1;
    }
}
