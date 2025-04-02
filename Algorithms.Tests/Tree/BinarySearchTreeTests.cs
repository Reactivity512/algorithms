namespace Algorithms.Tree.Tests;

using Xunit;

public sealed class BinarySearchTreeTests
{
    [Fact]
    public void Insert_ShouldAddElementsCorrectly()
    {
        var bst = new BinarySearchTree();
        bst.Insert(5);
        bst.Insert(3);
        bst.Insert(7);
        
        var inOrder = bst.InOrder();
        Assert.Equal(new[] { 3, 5, 7 }, inOrder);
    }

    [Fact]
    public void Contains_ShouldReturnCorrectResults()
    {
        var bst = new BinarySearchTree();
        bst.Insert(10);
        bst.Insert(5);
        bst.Insert(15);
        
        Assert.True(bst.Contains(10));
        Assert.True(bst.Contains(5));
        Assert.False(bst.Contains(20));
    }

    [Fact]
    public void Delete_LeafNode_ShouldRemoveCorrectly()
    {
        var bst = new BinarySearchTree();
        bst.Insert(10);
        bst.Insert(5);
        bst.Insert(15);
        bst.Delete(5);

        Assert.Equal(new[] { 10, 15 }, bst.InOrder());
    }

    [Fact]
    public void Delete_NodeWithOneChild_ShouldRemoveCorrectly()
    {
        var bst = new BinarySearchTree();
        bst.Insert(10);
        bst.Insert(5);
        bst.Insert(15);
        bst.Insert(12);
        bst.Delete(15);

        Assert.Equal(new[] { 5, 10, 12 }, bst.InOrder());
    }

    [Fact]
    public void Delete_NodeWithTwoChildren_ShouldRemoveCorrectly()
    {
        var bst = new BinarySearchTree();
        bst.Insert(10);
        bst.Insert(5);
        bst.Insert(15);
        bst.Insert(12);
        bst.Insert(17);
        bst.Delete(15);

        Assert.Equal(new[] { 5, 10, 12, 17 }, bst.InOrder());
    }

    [Fact]
    public void Traversals_ShouldReturnCorrectOrders()
    {
        var bst = new BinarySearchTree();
        bst.Insert(4);
        bst.Insert(2);
        bst.Insert(6);
        bst.Insert(1);
        bst.Insert(3);
        bst.Insert(5);
        bst.Insert(7);

        Assert.Equal(new[] { 1, 2, 3, 4, 5, 6, 7 }, bst.InOrder());
        Assert.Equal(new[] { 4, 2, 1, 3, 6, 5, 7 }, bst.PreOrder());
        Assert.Equal(new[] { 1, 3, 2, 5, 7, 6, 4 }, bst.PostOrder());
    }
}
