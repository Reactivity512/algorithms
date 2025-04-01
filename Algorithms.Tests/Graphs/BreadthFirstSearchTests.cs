namespace Algorithms.Graphs.Tests;

public sealed class BreadthFirstSearchTests
{
    [Fact]
    public void BFS_ConnectedGraph_VisitsAllVertices()
    {
        // Arrange
        var graph = new BreadthFirstSearch(5);
        graph.AddEdge(0, 1);
        graph.AddEdge(0, 2);
        graph.AddEdge(1, 3);
        graph.AddEdge(2, 4);
        
        // Act
        var result = graph.BFS(0);

        // Assert
        Assert.Equal(5, result.Count);
        Assert.Equal(new[] {0, 1, 2, 3, 4}, result);
    }

    [Fact]
    public void BFS_StartsFromMiddleNode_CorrectTraversalOrder()
    {
        // Arrange
        var graph = new BreadthFirstSearch(5);
        graph.AddEdge(0, 1);
        graph.AddEdge(1, 2);
        graph.AddEdge(2, 3);
        graph.AddEdge(3, 4);
        
        // Act
        var result = graph.BFS(2);
        List<int> expected = [2, 3, 4];

        // Assert
        Assert.Equal(expected, result);
    }

    // Для несвязного графа
    [Fact]
    public void BFS_DisconnectedGraph_VisitsOnlyConnectedComponent()
    {
        // Arrange
        var graph = new BreadthFirstSearch(6);
        graph.AddEdge(0, 1);
        graph.AddEdge(1, 2);
        graph.AddEdge(3, 4);
        graph.AddEdge(4, 5);
        
        // Act
        var result = graph.BFS(0);

        // Assert
        Assert.Equal(3, result.Count);
        Assert.Equal(new[] {0, 1, 2}, result);
    }

    [Fact]
    public void BFS_IsolatedNode_ReturnsOnlyThatNode()
    {
        // Arrange
        var graph = new BreadthFirstSearch(3);
        graph.AddEdge(0, 1);
        
        // Act
        var result = graph.BFS(2);

        // Assert
        Assert.Single(result);
        Assert.Equal(2, result[0]);
    }

    // Для графов с циклами
    [Fact]
    public void BFS_GraphWithCycle_VisitsAllNodesOnce()
    {
        // Arrange
        var graph = new BreadthFirstSearch(4);
        graph.AddEdge(0, 1);
        graph.AddEdge(1, 2);
        graph.AddEdge(2, 3);
        graph.AddEdge(3, 0);
        
        // Act
        var result = graph.BFS(0);

        // Assert
        Assert.Equal(4, result.Count);
        Assert.Equal(new[] {0, 1, 2, 3}, result);
    }

    [Fact]
    public void BFS_FullyConnectedGraph_CorrectOrder()
    {
        // Arrange
        var graph = new BreadthFirstSearch(4);
        for (int i = 0; i < 4; i++)
            for (int j = 0; j < 4; j++)
                if (i != j) 
                    graph.AddEdge(i, j);
        
        // Act
        var result = graph.BFS(0);

        // Assert
        Assert.Equal(4, result.Count);
        Assert.Equal(0, result[0]);
    }

    // Для граничных случаев
    [Fact]
    public void BFS_EmptyGraph_ThrowsException()
    {
        // Arrange
        var graph = new BreadthFirstSearch(0);
        
        // Act & Assert
        Assert.Throws<ArgumentOutOfRangeException>(() => graph.BFS(0));
    }

    [Fact]
    public void BFS_SingleNodeGraph_ReturnsThatNode()
    {
        // Arrange
        var graph = new BreadthFirstSearch(1);
        
        // Act
        var result = graph.BFS(0);

        // Assert
        Assert.Single(result);
        Assert.Equal(0, result[0]);
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(5)]
    public void BFS_InvalidStartNode_ThrowsException(int invalidNode)
    {
        // Arrange
        var graph = new BreadthFirstSearch(3);
        graph.AddEdge(0, 1);
        
        // Act & Assert
        Assert.Throws<ArgumentOutOfRangeException>(() => graph.BFS(invalidNode));
    }

    // Для проверки порядка обхода
    [Fact]
    public void BFS_LevelOrderTraversal_CorrectForBinaryTree()
    {
        // Arrange
        var graph = new BreadthFirstSearch(7);
        // Древовидная структура:
        //       0
        //     /   \
        //    1     2
        //   / \   / \
        //  3   4 5   6
        graph.AddEdge(0, 1);
        graph.AddEdge(0, 2);
        graph.AddEdge(1, 3);
        graph.AddEdge(1, 4);
        graph.AddEdge(2, 5);
        graph.AddEdge(2, 6);
        
        // Act
        var result = graph.BFS(0);

        // Assert
        Assert.Equal(0, result[0]);
        Assert.Equal(1, result[1]);
        Assert.Equal(2, result[2]);

        Assert.Contains(3, result.Skip(3).Take(4));
        Assert.Contains(4, result.Skip(3).Take(4));
        Assert.Contains(5, result.Skip(3).Take(4));
        Assert.Contains(6, result.Skip(3).Take(4));
    }

    // Для взвешенных графов (BFS без учета весов)
    [Fact]
    public void BFS_WeightedGraph_IgnoresWeights()
    {
        // Arrange
        var graph = new BreadthFirstSearch(4);
        // BFS игнорирует веса, поэтому мы просто проверяем связность
        graph.AddEdge(0, 1);
        graph.AddEdge(0, 2);
        graph.AddEdge(1, 3);
        
        // Act
        var result = graph.BFS(0);

        // Assert
        Assert.Equal(4, result.Count);
        Assert.Equal(0, result[0]);
        Assert.Equal(1, result[1]);
        Assert.Equal(2, result[2]);
        Assert.Equal(3, result[3]);
    }

    // Для ориентированных графов
    [Fact]
    public void BFS_DirectedGraph_TraversesOnlyReachable()
    {
        // Arrange
        var graph = new BreadthFirstSearch(5);
        graph.AddEdge(0, 1);
        graph.AddEdge(1, 2);
        graph.AddEdge(3, 4); // Недостижима из 0
        
        // Act
        var result = graph.BFS(0);

        // Assert
        Assert.Equal(3, result.Count);
        Assert.Equal(new[] {0, 1, 2}, result);
    }

    [Fact]
    public void BFS_DirectedGraph_ReverseEdges_NotTraversed()
    {
        // Arrange
        var graph = new BreadthFirstSearch(3);
        graph.AddEdge(0, 1);
        graph.AddEdge(2, 1); // Недостижима из 0
        
        // Act
        var result = graph.BFS(0);

        // Assert
        Assert.Equal(2, result.Count);
        Assert.Equal(new[] {0, 1}, result);
    }

    // Для проверки корректности меток посещения
    [Fact]
    public void BFS_DoesNotRevisitNodes()
    {
        // Arrange
        var graph = new BreadthFirstSearch(5);
        graph.AddEdge(0, 1);
        graph.AddEdge(0, 2);
        graph.AddEdge(1, 2);
        graph.AddEdge(2, 3);
        
        // Act
        var result = graph.BFS(0);

        // Assert
        Assert.Equal(result.Distinct().Count(), result.Count);
    }


    // Вспомогательный метод для создания тестового графа
    private BreadthFirstSearch CreateTestGraph()
    {
        var graph = new BreadthFirstSearch(6);
        graph.AddEdge(0, 1);
        graph.AddEdge(0, 2);
        graph.AddEdge(1, 3);
        graph.AddEdge(2, 4);
        graph.AddEdge(3, 5);
        graph.AddEdge(4, 5);
        return graph;
    }

    [Fact]
    public void AdvancedBFS_ShouldReturnCorrectOrder()
    {
        // Arrange
        var graph = CreateTestGraph();
        var expectedOrder = new[] { 0, 1, 2, 3, 4, 5 };

        // Act
        var result = graph.BFSAdvanced(0);

        // Assert
        Assert.Equal(expectedOrder, result.Order);
    }

    [Fact]
    public void AdvancedBFS_ShouldCalculateCorrectDistances()
    {
        // Arrange
        var graph = CreateTestGraph();

        // Act
        var result = graph.BFSAdvanced(0);

        // Assert
        Assert.Equal(0, result.Distances[0]);
        Assert.Equal(1, result.Distances[1]);
        Assert.Equal(1, result.Distances[2]);
        Assert.Equal(2, result.Distances[3]);
        Assert.Equal(2, result.Distances[4]);
        Assert.Equal(3, result.Distances[5]);
    }

    [Fact]
    public void AdvancedBFS_ShouldSetCorrectPredecessors()
    {
        // Arrange
        var graph = CreateTestGraph();

        // Act
        var result = graph.BFSAdvanced(0);

        // Assert
        Assert.Equal(-1, result.Predecessors[0]);
        Assert.Equal(0, result.Predecessors[1]);
        Assert.Equal(0, result.Predecessors[2]);
        Assert.Equal(1, result.Predecessors[3]);
        Assert.Equal(2, result.Predecessors[4]);
        Assert.True(result.Predecessors[5] == 3 || result.Predecessors[5] == 4); // Может быть два варианта
    }

    [Fact]
    public void AdvancedBFS_DisconnectedGraph_ShouldHaveInfiniteDistanceForUnreachable()
    {
        // Arrange
        var graph = new BreadthFirstSearch(4);
        graph.AddEdge(0, 1);
        graph.AddEdge(2, 3); // Отдельный компонент связности

        // Act
        var result = graph.BFSAdvanced(0);

        // Assert
        Assert.Equal(-1, result.Distances[2]);
        Assert.Equal(-1, result.Distances[3]);
        Assert.Equal(-1, result.Predecessors[2]);
        Assert.Equal(-1, result.Predecessors[3]);
    }

    [Fact]
    public void AdvancedBFS_ShouldHandleCyclesCorrectly()
    {
        // Arrange
        var graph = new BreadthFirstSearch(4);
        graph.AddEdge(0, 1);
        graph.AddEdge(1, 2);
        graph.AddEdge(2, 3);
        graph.AddEdge(3, 0);

        // Act
        var result = graph.BFSAdvanced(0);

        // Assert
        Assert.Equal(0, result.Distances[0]);
        Assert.Equal(1, result.Distances[1]);
        Assert.Equal(2, result.Distances[2]);
        Assert.Equal(3, result.Distances[3]);
    }

    [Fact]
    public void AdvancedBFS_StartFromMiddleNode_CorrectShortestPaths()
    {
        // Arrange
        var graph = CreateTestGraph();

        // Act
        var result = graph.BFSAdvanced(2);

        // Assert
        Assert.Equal(-1, result.Distances[0]); // Недостижима из 2
        Assert.Equal(-1, result.Distances[1]); // Недостижима из 2
        Assert.Equal(0, result.Distances[2]);
        Assert.Equal(-1, result.Distances[3]); // Недостижима из 2
        Assert.Equal(1, result.Distances[4]);
        Assert.Equal(2, result.Distances[5]);
    }

    [Fact]
    public void AdvancedBFS_SingleNodeGraph_ShouldHaveZeroDistance()
    {
        // Arrange
        var graph = new BreadthFirstSearch(1);

        // Act
        var result = graph.BFSAdvanced(0);

        // Assert
        Assert.Equal(0, result.Distances[0]);
        Assert.Equal(-1, result.Predecessors[0]);
        Assert.Single(result.Order);
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(10)]
    public void AdvancedBFS_InvalidStartNode_ThrowsException(int invalidNode)
    {
        // Arrange
        var graph = new BreadthFirstSearch(3);

        // Act & Assert
        Assert.Throws<ArgumentOutOfRangeException>(() => graph.BFSAdvanced(invalidNode));
    }

    [Fact]
    public void AdvancedBFS_ShouldFindShortestPathInUnweightedGraph()
    {
        // Arrange
        var graph = new BreadthFirstSearch(7);
        graph.AddEdge(0, 1);
        graph.AddEdge(1, 2);
        graph.AddEdge(2, 3);
        graph.AddEdge(0, 4);
        graph.AddEdge(4, 5);
        graph.AddEdge(5, 3);

        // Act
        var result = graph.BFSAdvanced(0);

        // Assert
        Assert.Equal(3, result.Distances[3]);
        Assert.True(result.Predecessors[3] == 2 || result.Predecessors[3] == 5);
    }
}
