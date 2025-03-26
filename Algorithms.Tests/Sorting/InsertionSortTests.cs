namespace Algorithms.Tests;

public sealed class InsertionSortTests
{
    // Вспомогательный метод для проверки сортировки
    private static void TestSort(int[] input, int[] expected)
    {
        Sorting.InsertionSort.Sort(input);

        Assert.Equal(expected, input);
    }

    // Тест 1: Сортировка массива с несколькими элементами
    [Fact]
    public void Sort_ArrayWithMultipleElements_ReturnsSortedArray()
    {
        int[] input = [12, 11, 13, 5, 6];
        int[] expected = [5, 6, 11, 12, 13];

        TestSort(input, expected);
    }

    // Тест 2: Сортировка уже отсортированного массива
    [Fact]
    public void Sort_AlreadySortedArray_ReturnsSameArray()
    {
        int[] input = [1, 2, 3, 4, 5];
        int[] expected = [1, 2, 3, 4, 5];

        TestSort(input, expected);
    }

    // Тест 3: Сортировка массива с одним элементом
    [Fact]
    public void Sort_SingleElementArray_ReturnsSameArray()
    {
        int[] input = [42];
        int[] expected = [42];

        TestSort(input, expected);
    }

    // Тест 4: Сортировка пустого массива
    [Fact]
    public void Sort_EmptyArray_ReturnsEmptyArray()
    {
        int[] input = [];
        int[] expected = [];

        TestSort(input, expected);
    }

    // Тест 5: Сортировка массива с отрицательными числами
    [Fact]
    public void Sort_ArrayWithNegativeNumbers_ReturnsSortedArray()
    {
        int[] input = [-5, -1, -10, 0, 3];
        int[] expected = [-10, -5, -1, 0, 3];

        TestSort(input, expected);
    }

    // Тест 6: Сортировка массива с дубликатами
    [Fact]
    public void Sort_ArrayWithDuplicates_ReturnsSortedArray()
    {
        int[] input = [5, 3, 5, 1, 2];
        int[] expected = [1, 2, 3, 5, 5];

        TestSort(input, expected);
    }

    // Тест 7: Сортировка null-массива
    [Fact]
    public void Sort_NullArray_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() => Sorting.InsertionSort.Sort(null));
    }
}
