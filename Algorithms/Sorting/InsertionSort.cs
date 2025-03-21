namespace Algorithms.Sorting;

public static class InsertionSort
{
    public static void Sort(int[] array)
    {
        if (array == null)
        {
            throw new ArgumentNullException(nameof(array), "Name cannot be null.");
        }

        if (array.Length <= 1)
            return;

        for (int i = 1; i < array.Length; i++)
        {
            int key = array[i]; // Текущий элемент, который нужно вставить
            int j = i - 1;

            // Сдвигаем элементы больше key вправо
            while (j >= 0 && array[j] > key)
            {
                array[j + 1] = array[j];
                j--;
            }

            // Вставляем key на правильное место
            array[j + 1] = key;
        }
    }
}
