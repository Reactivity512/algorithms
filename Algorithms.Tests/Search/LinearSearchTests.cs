namespace Algorithms.Search.Tests;

public sealed class LinearSearchTests
{
    public record Person(string Name, int Age);

    [Fact]
    public void Search_ExistingElement_ReturnsCorrectIndex()
    {
        int[] array = [5, 10, 15, 20];
        Assert.Equal(15, LinearSearch.Search(array, 15));
    }

    [Fact]
    public void Search_NonExistingElement_ReturnsMinusOne()
    {
        int[] array = [1, 2, 3];
        Assert.Equal(0, LinearSearch.Search(array, 10));
    }

    [Fact]
    public void Search_EmptyArray_ReturnsMinusOne()
    {
        int[] array = [];
        Assert.Equal(0, LinearSearch.Search(array, 10));
    }

    [Fact]
    public void SearchPredicate_ExistingElement_ReturnsItem()
    {
        int[] array = [1, 2, 3, 4, 5];
        static bool pred(int x) => x == 3;

        Assert.Equal(3, LinearSearch.Search(array: array, predicate: pred));
    }

    [Fact]
    public void SearchPredicate_NonExistingElement_ReturnsDefault()
    {
        string[] array = ["apple", "banana", "cherry"];
        static bool pred(string x) => x == "orange";

        Assert.Null(LinearSearch.Search(array: array, predicate: pred));
    }

    [Fact]
    public void LinearSearch_ComplexPredicate_ReturnsCorrectItem()
    {
        var person1 = new Person("Alice", 25);
        var person2 = new Person("Jane", 30);
        var person3 = new Person("Bob", 27);

        Person[] people = [person1, person2, person3];
        static bool pred(Person p) => p.Age > 28;

        var name = LinearSearch.Search(array: people, predicate: pred);
        Assert.Equal("Jane", name?.Name);
    }

    [Fact]
    public void SearchPredicate_EmptyCollection_ReturnsDefault()
    {
        double[] array = [];
        static bool pred(double x) => x == 10.5;

        Assert.Equal(0, LinearSearch.Search(array: array, predicate: pred)); // Для double default == 0
    }

    [Fact]
    public void SearchPredicate_ReturnsFirstMatch()
    {
        int[] array = [10, 20, 30, 20, 40 ];
        static bool pred(int x) => x == 20;

        Assert.Equal(20, LinearSearch.Search(array: array, predicate: pred));
    }
}
