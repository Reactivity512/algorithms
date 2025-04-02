namespace Algorithms.Tree.Tests;

using Xunit;

public sealed class RedBlackTreeTests
{
    [Fact]
    public void Insert_SingleElement_ShouldBecomeRoot()
    {
        var tree = new RedBlackTree();
        tree.Insert(10);

        Assert.Equal(new[] { 10 }, tree.InOrder());
        Assert.True(tree.Contains(10));
        Assert.False(tree.Contains(20));
    }

    [Fact]
    public void Insert_ShouldMaintainRedBlackProperties()
    {
        var tree = new RedBlackTree();
        tree.Insert(10);
        tree.Insert(20);
        tree.Insert(30);
        tree.Insert(15);
        tree.Insert(25);
        
        Assert.Equal(new[] { 10, 15, 20, 25, 30 },  tree.InOrder());
    }

    [Fact]
    public void Delete_ShouldMaintainRedBlackProperties()
    {
        var tree = new RedBlackTree();
        tree.Insert(10);
        tree.Insert(5);
        tree.Insert(15);
        tree.Insert(20);
        
        tree.Delete(5);

        Assert.Equal(new[] { 10, 15, 20 }, tree.InOrder());
        Assert.False(tree.Contains(5));
    }

    [Fact]
    public void Delete_NodeWithOneChild_ShouldMaintainProperties()
    {
        var tree = new RedBlackTree();
        tree.Insert(10);
        tree.Insert(5);
        tree.Insert(15);
        tree.Insert(12);
        
        tree.Delete(15);

        Assert.Equal(new[] { 5, 10, 12 }, tree.InOrder());
        Assert.False(tree.Contains(15));
    }

    [Fact]
    public void Contains_ShouldFindAllInsertedValues()
    {
        var tree = new RedBlackTree();
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
        var tree = new RedBlackTree();
        tree.Insert(4);
        tree.Insert(2);
        tree.Insert(6);
        tree.Insert(1);
        tree.Insert(3);
        tree.Insert(5);
        tree.Insert(7);

        Assert.Equal(new[] { 1, 2, 3, 4, 5, 6, 7 }, tree.InOrder());
        Assert.Equal(7, tree.PreOrder().Count);
        Assert.Equal(7, tree.PostOrder().Count);
    }
}
