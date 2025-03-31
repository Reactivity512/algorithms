namespace Algorithms.Graphs.Tests;

public sealed class DijkstraAlgorithmTests
{
    // Вспомогательный метод для создания графа
    private static Dictionary<int, Dictionary<int, int>> CreateTestGraph()
    {
        return new Dictionary<int, Dictionary<int, int>>
        {
            [0] = new Dictionary<int, int> { [1] = 4, [2] = 1 },
            [1] = new Dictionary<int, int> { [3] = 1 },
            [2] = new Dictionary<int, int> { [1] = 2, [3] = 5 },
            [3] = new Dictionary<int, int> { }
        };
    }

    // Корректный расчет расстояний
    [Fact]
    public void FindShortestPaths_ValidGraph_ReturnsCorrectDistances()
    {
        // Arrange
        var graph = CreateTestGraph();
        var dijkstra = new DijkstraAlgorithm(graph);

        // Act
        var distances = dijkstra.FindShortestPaths(0);

        // Assert
        Assert.Equal(0, distances[0]);
        Assert.Equal(3, distances[1]);
        Assert.Equal(1, distances[2]);
        Assert.Equal(4, distances[3]);
    }

    // Граф с одной вершиной
    [Fact]
    public void FindShortestPaths_SingleVertex_ReturnsZeroDistance()
    {
        // Arrange
        var graph = new Dictionary<int, Dictionary<int, int>>
        {
            [0] = []
        };
        var dijkstra = new DijkstraAlgorithm(graph);

        // Act
        var distances = dijkstra.FindShortestPaths(0);

        // Assert
        Assert.Single(distances);
        Assert.Equal(0, distances[0]);
    }

    // Недостижимые вершины
    [Fact]
    public void FindShortestPaths_UnreachableVertices_ReturnsInfinity()
    {
        // Arrange
        var graph = new Dictionary<int, Dictionary<int, int>>
        {
            [0] = new Dictionary<int, int> { [1] = 2 },
            [1] = new Dictionary<int, int> { },
            [2] = new Dictionary<int, int> { } // Вершина 2 недостижима
        };
        var dijkstra = new DijkstraAlgorithm(graph);

        // Act
        var distances = dijkstra.FindShortestPaths(0);

        // Assert
        Assert.Equal(int.MaxValue, distances[2]);
    }

    // Ошибка при отрицательных весах
    [Fact]
    public void FindShortestPaths_NegativeWeights_ThrowsException()
    {
        // Arrange
        var graph = new Dictionary<int, Dictionary<int, int>>
        {
            [0] = new Dictionary<int, int> { [1] = -1 } // Отрицательный вес
        };
        var dijkstra = new DijkstraAlgorithm(graph);

        // Act & Assert
        Assert.Throws<InvalidOperationException>(() => dijkstra.FindShortestPaths(0));
    }

    // Восстановление путей
    [Fact]
    public void FindPaths_ValidGraph_ReturnsCorrectPaths()
    {
        // Arrange
        var graph = CreateTestGraph();
        var dijkstra = new DijkstraAlgorithm(graph);

        // Act
        var paths = dijkstra.FindPaths(0);

        // Assert
        Assert.Equal(new List<int> { 0 }, paths[0]);
        Assert.Equal(new List<int> { 0, 2, 1 }, paths[1]);
        Assert.Equal(new List<int> { 0, 2 }, paths[2]);
        Assert.Equal(new List<int> { 0, 2, 1, 3 }, paths[3]);
    }
}
