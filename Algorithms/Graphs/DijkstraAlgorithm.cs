namespace Algorithms.Graphs;

public sealed class DijkstraAlgorithm(Dictionary<int, Dictionary<int, int>> graph)
{
    // Граф представлен как словарь: вершина -> (сосед, вес)
    private readonly Dictionary<int, Dictionary<int, int>> graph = graph;

    public Dictionary<int, List<int>> FindPaths(int start)
    {
        var previous = new Dictionary<int, int>(); // Хранит предков
        var distances = new Dictionary<int, int>();
        var priorityQueue = new PriorityQueue<int, int>();

        foreach (var vertex in graph.Keys)
        {
            distances[vertex] = int.MaxValue;
            previous[vertex] = -1; // -1 = нет предка
        }

        distances[start] = 0;
        priorityQueue.Enqueue(start, 0);

        while (priorityQueue.Count > 0)
        {
            var current = priorityQueue.Dequeue();

            foreach (var neighbor in graph[current])
            {
                int newDist = distances[current] + neighbor.Value;
                if (newDist < distances[neighbor.Key])
                {
                    distances[neighbor.Key] = newDist;
                    previous[neighbor.Key] = current;
                    priorityQueue.Enqueue(neighbor.Key, newDist);
                }
            }
        }

        // Восстановление путей
        var paths = new Dictionary<int, List<int>>();
        foreach (var vertex in graph.Keys)
        {
            var path = new List<int>();
            int current = vertex;
            while (current != -1)
            {
                path.Add(current);
                current = previous[current];
            }
            path.Reverse();
            paths[vertex] = path;
        }

        return paths;
    }

    public Dictionary<int, int> FindShortestPaths(int start)
    {
        var distances = new Dictionary<int, int>();
        var priorityQueue = new PriorityQueue<int, int>();

        // Инициализация расстояний
        foreach (var vertex in graph.Keys)
        {
            distances[vertex] = int.MaxValue;
        }
        distances[start] = 0;
        priorityQueue.Enqueue(start, 0);

        while (priorityQueue.Count > 0)
        {
            var currentVertex = priorityQueue.Dequeue();

            foreach (var (neighbor, weight) in graph[currentVertex])
            {
                if (weight < 0)
                    throw new InvalidOperationException("Граф содержит отрицательные веса!");
                    
                int newDistance = distances[currentVertex] + weight;

                if (newDistance < distances[neighbor])
                {
                    distances[neighbor] = newDistance;
                    priorityQueue.Enqueue(neighbor, newDistance); // Автоматическая сортировка
                }
            }
        }

        return distances;
    }

}
