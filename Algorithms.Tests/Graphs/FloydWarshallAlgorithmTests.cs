namespace Algorithms.Graphs.Tests;

public sealed class FloydWarshallAlgorithmTests
{
    // Граф без отрицательных весов
    [Fact]
    public static void FindAllShortestPaths_NoNegativeWeights_ReturnsCorrectDistances()
    {
        // Arrange
        int[,] graph = {
            { 0, 3, int.MaxValue, 5 },
            { 2, 0, int.MaxValue, 4 },
            { int.MaxValue, 1, 0, int.MaxValue },
            { int.MaxValue, int.MaxValue, 2, 0 }
        };

        // Act
        int[,] result = FloydWarshallAlgorithm.FindAllShortestPaths(graph);

        // Assert
        Assert.Equal(0, result[0, 0]);
        Assert.Equal(3, result[0, 1]);
        Assert.Equal(5, result[0, 3]);
        Assert.Equal(5, result[2, 3]);
    }

    // Граф с отрицательными весами (без отрицательных циклов)
    [Fact]
    public static void FindAllShortestPaths_NegativeWeights_ReturnsCorrectDistances()
    {
        // Arrange
        int[,] graph = {
            { 0, 1, int.MaxValue },
            { int.MaxValue, 0, -5 },
            { 2, int.MaxValue, 0 }
        };

        // Act & Assert
        Assert.Throws<InvalidOperationException>(() =>
            FloydWarshallAlgorithm.FindAllShortestPaths(graph));
    }

    // Граф с отрицательным циклом
    [Fact]
    public void FindAllShortestPaths_NegativeCycle_ThrowsException()
    {
        // Arrange
        int[,] graph = {
            { 0, 1, int.MaxValue },
            { int.MaxValue, 0, -5 },
            { 2, -10, 0 } // Отрицательный цикл: 2 → 1 → 2 (2 + (-10) + (-5) = -13)
        };

        // Act & Assert
        Assert.Throws<InvalidOperationException>(() =>
            FloydWarshallAlgorithm.FindAllShortestPaths(graph));
    }

    // Пустой граф
    [Fact]
    public void FindAllShortestPaths_EmptyGraph_ReturnsEmptyMatrix()
    {
        // Arrange
        int[,] graph = new int[0, 0];

        // Act
        int[,] result = FloydWarshallAlgorithm.FindAllShortestPaths(graph);

        // Assert
        Assert.Empty(result);
    }

    // Граф с одной вершиной
    [Fact]
    public void FindAllShortestPaths_SingleVertex_ReturnsZeroDistance()
    {
        // Arrange
        int[,] graph = { { 0 } };

        // Act
        int[,] result = FloydWarshallAlgorithm.FindAllShortestPaths(graph);

        // Assert
        Assert.Equal(0, result[0, 0]);
    }
}
