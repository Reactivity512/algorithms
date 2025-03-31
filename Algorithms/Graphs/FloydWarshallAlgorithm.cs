namespace Algorithms.Graphs;

public class FloydWarshallAlgorithm
{
    public static int[,] FindAllShortestPaths(int[,] graph)
    {
        int vertices = graph.GetLength(0);
        int[,] distances = (int[,])graph.Clone();

        for (int k = 0; k < vertices; k++)
        {
            for (int i = 0; i < vertices; i++)
            {
                for (int j = 0; j < vertices; j++)
                {
                    if (distances[i, k] != int.MaxValue && 
                        distances[k, j] != int.MaxValue)
                    {
                        int newDistance = distances[i, k] + distances[k, j];
                        if (newDistance < distances[i, j])
                        {
                            distances[i, j] = newDistance;
                        }
                    }
                }
            }
        }

        // Проверка на отрицательные циклы
        for (int i = 0; i < vertices; i++)
        {
            if (distances[i, i] < 0)
            {
                throw new InvalidOperationException("Граф содержит отрицательный цикл!");
            }
        }

        return distances;
    }
}
