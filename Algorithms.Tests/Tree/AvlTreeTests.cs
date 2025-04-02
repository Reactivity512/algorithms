namespace Algorithms.Tree.Tests;

using Xunit;

public sealed class AvlTreeTests
{
    [Fact]
    public void Insert_ShouldBalanceTree()
    {
        var tree = new AvlTree();
        tree.Insert(10);
        tree.Insert(20);
        tree.Insert(30); // Должен балансироваться
        
        var inOrder = tree.InOrder();
        Assert.Equal(new[] { 10, 20, 30 }, inOrder);
    }

    [Fact]
    public void Delete_ShouldMaintainBalance()
    {
        var tree = new AvlTree();
        tree.Insert(10);
        tree.Insert(5);
        tree.Insert(15);
        tree.Insert(20);
        
        tree.Delete(5); // Должен оставаться сбалансированным

        Assert.Equal(new[] { 10, 15, 20 }, tree.InOrder());
    }

    [Fact]
    public void Contains_ShouldFindAllInsertedValues()
    {
        var tree = new AvlTree();
        tree.Insert(5);
        tree.Insert(3);
        tree.Insert(7);
        tree.Insert(2);
        tree.Insert(4);
        
        Assert.True(tree.Contains(5));
        Assert.True(tree.Contains(3));
        Assert.True(tree.Contains(7));
        Assert.True(tree.Contains(2));
        Assert.True(tree.Contains(4));
        Assert.False(tree.Contains(10));
    }

    [Fact]
    public void Traversals_ShouldReturnCorrectOrders()
    {
        var tree = new AvlTree();
        tree.Insert(4);
        tree.Insert(2);
        tree.Insert(6);
        tree.Insert(1);
        tree.Insert(3);
        tree.Insert(5);
        tree.Insert(7);

        Assert.Equal(new[] { 1, 2, 3, 4, 5, 6, 7 }, tree.InOrder());
        Assert.Equal(new[] { 4, 2, 1, 3, 6, 5, 7 }, tree.PreOrder());
        Assert.Equal(new[] { 1, 3, 2, 5, 7, 6, 4 }, tree.PostOrder());
    }
}
