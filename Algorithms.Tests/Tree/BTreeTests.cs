namespace Algorithms.Tree.Tests;

using Xunit;

public sealed class BTreeTests
{
    [Fact]
    public void Insert_Search_ShouldWorkCorrectly()
    {
        var tree = new BTree(2);
        tree.Insert(10);
        tree.Insert(20);
        tree.Insert(5);
        
        Assert.True(tree.Contains(10));
        Assert.True(tree.Contains(20));
        Assert.True(tree.Contains(5));
        Assert.False(tree.Contains(15));
    }

    [Fact]
    public void Delete_ShouldMaintainTreeProperties()
    {
        var tree = new BTree(3);
        tree.Insert(10);
        tree.Insert(20);
        tree.Insert(5);
        tree.Insert(6);
        tree.Insert(12);
        
        tree.Delete(5);
        tree.Delete(20);
        
        Assert.False(tree.Contains(5));
        Assert.False(tree.Contains(20));
        Assert.Equal(new[] { 6, 10, 12 }, tree.Traverse());
    }

    [Fact]
    public void Traverse_ShouldReturnSortedSequence()
    {
        var tree = new BTree(2);
        tree.Insert(5);
        tree.Insert(3);
        tree.Insert(7);
        tree.Insert(2);
        tree.Insert(4);
        tree.Insert(6);
        tree.Insert(8);

        Assert.Equal(new[] { 2, 3, 4, 5, 6, 7, 8 }, tree.Traverse());
    }

    [Fact]
    public void LargeNumberOfElements_ShouldMaintainStructure()
    {
        var tree = new BTree(5);
        var values = new HashSet<int>();
        
        // Гарантированно уникальные значения
        for (int i = 0; i < 1000; i++)
        {
            int val = i * 10; // или другой способ генерации уникальных значений
            values.Add(val);
            tree.Insert(val);
        }
        
        // Проверка всех добавленных значений
        foreach (var val in values)
        {
            Assert.True(tree.Contains(val));
        }
        
        var traversed = tree.Traverse();
        Assert.Equal(values.Count, traversed.Count);
        
        // Проверка сортировки
        for (int i = 1; i < traversed.Count; i++)
        {
            Assert.True(traversed[i-1] < traversed[i]);
        }
    }
}
