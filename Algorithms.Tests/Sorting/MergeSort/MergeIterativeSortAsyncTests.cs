namespace Algorithms.Tests;

public sealed class MergeIterativeSortAsyncTests
{
    // Вспомогательный метод для проверки сортировки
    private static async Task TestSortAsync(int[] input, int[] expected)
    {
        await Sorting.MergeSortAsync.IterativeSortAsync(input);

        Assert.Equal(expected, input);
    }

    // Тест 1: Сортировка массива с несколькими элементами
    [Fact]
    public async Task Sort_ArrayWithMultipleElements_ReturnsSortedArray()
    {
        int[] input = [38, 27, 43, 3, 9, 82, 10];
        int[] expected = [3, 9, 10, 27, 38, 43, 82];

        await TestSortAsync(input, expected);
    }

    // Тест 2: Сортировка уже отсортированного массива
    [Fact]
    public async Task Sort_AlreadySortedArray_ReturnsSameArray()
    {
        int[] input = [1, 2, 3, 4, 5];
        int[] expected = [1, 2, 3, 4, 5];

        await TestSortAsync(input, expected);
    }

    // Тест 3: Сортировка массива с одним элементом
    [Fact]
    public async Task Sort_SingleElementArray_ReturnsSameArray()
    {
        int[] input = [42];
        int[] expected = [42];

        await TestSortAsync(input, expected);
    }

    // Тест 4: Сортировка пустого массива
    [Fact]
    public async Task Sort_EmptyArray_ReturnsEmptyArray()
    {
        int[] input = [];
        int[] expected = [];

        await TestSortAsync(input, expected);
    }

    // Тест 5: Сортировка массива с отрицательными числами
    [Fact]
    public async Task Sort_ArrayWithNegativeNumbers_ReturnsSortedArray()
    {
        int[] input = [-5, -1, -10, 0, 3];
        int[] expected = [-10, -5, -1, 0, 3];

        await TestSortAsync(input, expected);
    }

    // Тест 6: Сортировка массива с дубликатами
    [Fact]
    public async Task Sort_ArrayWithDuplicates_ReturnsSortedArray()
    {
        int[] input = [5, 3, 5, 1, 2];
        int[] expected = [1, 2, 3, 5, 5];

        await TestSortAsync(input, expected);
    }

    // Тест 7: Сортировка null-массива
    [Fact]
    public async Task Sort_NullArray_ThrowsArgumentNullException()
    {
        await Assert.ThrowsAsync<ArgumentNullException>(() => Sorting.MergeSortAsync.IterativeSortAsync(null));
    }
}
