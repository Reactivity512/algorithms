namespace Algorithms.Graphs.Tests;

public sealed class DepthFirstSearchTest
{
     private static DepthFirstSearch CreateTestGraph()
     {
        DepthFirstSearch g = new(6);
        g.AddEdge(0, 1);
        g.AddEdge(0, 2);
        g.AddEdge(1, 3);
        g.AddEdge(2, 4);
        g.AddEdge(3, 5);
        g.AddEdge(4, 5);
        return g;
    }

    private static string RunDFS(DepthFirstSearch g, int startVertex)
    {
        var consoleOutput = new StringWriter();
        Console.SetOut(consoleOutput);
        g.DFSRecursive(startVertex);
        return consoleOutput.ToString().Trim();
    }

    // Обход из вершины 0
    [Fact]
    public void DFS_FromVertex0_ReturnsCorrectOrder()
    {
        // Arrange
        var g = CreateTestGraph();
        string expected = "0 1 3 5 2 4";

        // Act
        string result = RunDFS(g, 0);

        // Assert
        Assert.Equal(expected, result);
    }

    // Обход из вершины 2
    [Fact]
    public void DFS_FromVertex2_ReturnsCorrectOrder()
    {
        // Arrange
        var g = CreateTestGraph();
        string expected = "2 4 5";

        // Act
        string result = RunDFS(g, 2);

        // Assert
        Assert.Equal(expected, result);
    }

    // Обход изолированной вершины
    [Fact]
    public void DFS_FromIsolatedVertex_ReturnsOnlyThatVertex()
    {
        // Arrange
        var g = new DepthFirstSearch(3);
        g.AddEdge(0, 1);
        // Вершина 2 изолирована
        string expected = "2";

        // Act
        string result = RunDFS(g, 2);

        // Assert
        Assert.Equal(expected, result);
    }

    // Пустой граф
    [Fact]
    public void DFS_EmptyGraph_ThrowsException()
    {
        // Arrange
        var g = new DepthFirstSearch(0);

        // Act & Assert
        Assert.Throws<ArgumentOutOfRangeException>(() => g.DFSRecursive(0));
    }

    // Граф с циклом
    [Fact]
    public void DFS_GraphWithCycle_ReturnsAllVertices()
    {
        // Arrange
        var g = new DepthFirstSearch(3);
        g.AddEdge(0, 1);
        g.AddEdge(1, 2);
        g.AddEdge(2, 0);
        string expected = "0 1 2";

        // Act
        string result = RunDFS(g, 0);

        // Assert
        Assert.Equal(expected, result);
    }

    // Несуществующая стартовая вершина
    [Fact]
    public void DFS_InvalidStartVertex_ThrowsException()
    {
        // Arrange
        var g = CreateTestGraph();

        // Act & Assert
        Assert.Throws<ArgumentOutOfRangeException>(() => g.DFSRecursive(10));
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(10)]
    public void DFS_InvalidStartVertex_ThrowsArgumentOutOfRange(int invalidVertex)
    {
        var g = CreateTestGraph();
        var ex = Assert.Throws<ArgumentOutOfRangeException>(() => g.DFSIterative(invalidVertex));
        Assert.Contains("Vertex index must be between", ex.Message);
    }

    // Тест для итеративной версии DFS
    [Fact]
    public void IterativeDFS_FromVertex0_ReturnsCorrectOrder()
    {
        // Arrange
        var g = new DepthFirstSearch(5);
        g.AddEdge(0, 1);
        g.AddEdge(0, 2);
        g.AddEdge(1, 3);
        g.AddEdge(2, 4);
        string expected = "0 1 3 2 4";

        var consoleOutput = new StringWriter();
        Console.SetOut(consoleOutput);
        
        // Act
        g.DFSIterative(0); // Используем итеративную версию
        string result = consoleOutput.ToString().Trim();

        // Assert
        Assert.Equal(expected, result);
    }
}
