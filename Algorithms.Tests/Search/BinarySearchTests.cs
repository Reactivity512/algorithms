namespace Algorithms.Search.Tests;

public sealed class BinarySearchTests
{
    public record Person(string Name, int Age);

    [Fact]
    public void Search_ExistingElement_ReturnsCorrectIndex()
    {
        int[] array = [1, 2, 3, 4, 5];
        int index = BinarySearch.Search(array, 3);
        Assert.Equal(2, index);
    }

    [Fact]
    public void Search_NonExistingElement_ReturnsMinusOne()
    {
        int[] array = [10, 20, 30];
        int index = BinarySearch.Search(array, 25);
        Assert.Equal(-1, index);
    }

    [Fact]
    public void Search_EmptyArray_ReturnsMinusOne()
    {
        int[] array = [];
        int index = BinarySearch.Search(array, 10);
        Assert.Equal(-1, index);
    }

    [Fact]
    public void SearchPredicate_CustomComparer_WorksCorrectly()
    {
        string[] array = ["a", "b", "c"];
        int index = BinarySearch.Search(array, "B", StringComparer.OrdinalIgnoreCase.Compare);
        Assert.Equal(1, index);
    }

    [Fact]
    public void SearchPredicate_ComplexPredicate_ReturnsCorrectItem()
    {
        var person1 = new Person("Alice", 25);
        var person2 = new Person("Jane", 27);
        var person3 = new Person("Bob", 30);

        Person[] people = [person1, person2, person3];
        static bool pred(Person p) => p.Age > 28;

        var name = LinearSearch.Search(array: people, predicate: pred);
        Assert.Equal("Bob", name?.Name);
    }
}
