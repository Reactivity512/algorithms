namespace Algorithms.Tests;

public class QuickRecursiveSortAsyncTests
{
    // Вспомогательный метод для проверки сортировки
    private static async Task TestSortAsync(int[] input, int[] expected)
    {
        await Sorting.QuickSortAsync.RecursiveSortAsync(input);

        Assert.Equal(expected, input);
    }

    // Тест 1: Сортировка массива с несколькими элементами
    [Fact]
    public async Task Sort_ArrayWithMultipleElements_ReturnsSortedArray()
    {
        int[] input = [10, 7, 8, 9, 1, 5];
        int[] expected = [1, 5, 7, 8, 9, 10];

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
        await Assert.ThrowsAsync<ArgumentNullException>(() => Sorting.QuickSortAsync.RecursiveSortAsync(null));
    }
}
